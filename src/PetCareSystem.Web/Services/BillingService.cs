using Microsoft.EntityFrameworkCore;
using PetCareSystem.Web.Data;
using PetCareSystem.Web.Models;

namespace PetCareSystem.Web.Services;

public class BillingService
{
    private readonly ApplicationDbContext _db;

    public BillingService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Invoice> GenerateAppointmentInvoiceAsync(
        int appointmentId,
        decimal taxAmount = 0,
        decimal discountAmount = 0)
    {
        if (taxAmount < 0 || discountAmount < 0)
            throw new InvalidOperationException("Tax and discount cannot be negative.");

        await using var transaction = await _db.Database.BeginTransactionAsync();

        var appointment = await _db.Set<Appointment>()
            .FirstOrDefaultAsync(a => a.AppointmentId == appointmentId);

        if (appointment == null)
            throw new InvalidOperationException("Appointment not found.");

        if (await _db.Invoices.AnyAsync(i => i.AppointmentId == appointmentId))
            throw new InvalidOperationException("An invoice already exists for this appointment.");

        var services = await _db.Set<AppointmentService>()
            .Where(a => a.AppointmentId == appointmentId)
            .ToListAsync();

        if (services.Count == 0)
            throw new InvalidOperationException("Add at least one service before generating an invoice.");

        if (services.Any(s => s.Quantity <= 0 || s.UnitPrice < 0))
            throw new InvalidOperationException("Invalid service quantity or price.");

        var subtotal = services.Sum(s => s.Quantity * s.UnitPrice);

        if (discountAmount > subtotal + taxAmount)
            throw new InvalidOperationException("Discount cannot exceed the invoice amount.");

        var invoice = new Invoice
        {
            InvoiceNumber = $"TEMP-{Guid.NewGuid():N}",
            OwnerProfileId = appointment.OwnerProfileId,
            AppointmentId = appointmentId,
            SubTotal = subtotal,
            TaxAmount = taxAmount,
            DiscountAmount = discountAmount,
            TotalAmount = subtotal + taxAmount - discountAmount,
            Status = "Pending"
        };

        _db.Invoices.Add(invoice);
        await _db.SaveChangesAsync();

        invoice.InvoiceNumber = $"INV-{invoice.CreatedAt.Year}-{invoice.InvoiceId:D6}";

        foreach (var service in services)
        {
            _db.InvoiceItems.Add(new InvoiceItem
            {
                InvoiceId = invoice.InvoiceId,
                Description = service.Service?.ServiceName ?? $"Service #{service.ServiceId}",
                Quantity = service.Quantity,
                UnitPrice = service.UnitPrice,
                TotalPrice = service.Quantity * service.UnitPrice
            });
        }

        await _db.SaveChangesAsync();
        await transaction.CommitAsync();

        return invoice;
    }

    public async Task<Invoice> GenerateEmergencyInvoiceAsync(
        int emergencyCaseId,
        decimal treatmentCharge = 0,
        decimal taxAmount = 0,
        decimal discountAmount = 0)
    {
        if (treatmentCharge < 0 || taxAmount < 0 || discountAmount < 0)
            throw new InvalidOperationException("Charges, tax and discount cannot be negative.");

        await using var transaction = await _db.Database.BeginTransactionAsync();

        var emergencyCase = await _db.EmergencyCases
            .Include(e => e.Veterinarian)
            .FirstOrDefaultAsync(e => e.EmergencyCaseId == emergencyCaseId);

        if (emergencyCase == null)
            throw new InvalidOperationException("Emergency case not found.");

        if (await _db.Invoices.AnyAsync(i => i.EmergencyCaseId == emergencyCaseId))
            throw new InvalidOperationException("An invoice already exists for this emergency case.");

        if (emergencyCase.Veterinarian == null)
            throw new InvalidOperationException("Assign a veterinarian before generating the invoice.");

        var consultationFee = emergencyCase.Veterinarian.ConsultationFee;
        var subtotal = consultationFee + treatmentCharge;

        if (subtotal <= 0)
            throw new InvalidOperationException("The invoice must have a charge greater than zero.");

        if (discountAmount > subtotal + taxAmount)
            throw new InvalidOperationException("Discount cannot exceed the invoice amount.");

        var invoice = new Invoice
        {
            InvoiceNumber = $"TEMP-{Guid.NewGuid():N}",
            OwnerProfileId = emergencyCase.OwnerProfileId,
            EmergencyCaseId = emergencyCaseId,
            SubTotal = subtotal,
            TaxAmount = taxAmount,
            DiscountAmount = discountAmount,
            TotalAmount = subtotal + taxAmount - discountAmount,
            Status = "Pending"
        };

        _db.Invoices.Add(invoice);
        await _db.SaveChangesAsync();

        invoice.InvoiceNumber = $"INV-{invoice.CreatedAt.Year}-{invoice.InvoiceId:D6}";

        _db.InvoiceItems.Add(new InvoiceItem
        {
            InvoiceId = invoice.InvoiceId,
            Description = "Emergency Consultation",
            Quantity = 1,
            UnitPrice = consultationFee,
            TotalPrice = consultationFee
        });

        if (treatmentCharge > 0)
        {
            _db.InvoiceItems.Add(new InvoiceItem
            {
                InvoiceId = invoice.InvoiceId,
                Description = "Emergency Treatment Charges",
                Quantity = 1,
                UnitPrice = treatmentCharge,
                TotalPrice = treatmentCharge
            });
        }

        await _db.SaveChangesAsync();
        await transaction.CommitAsync();

        return invoice;
    }
    public async Task<Payment> RecordPaymentAsync(
        int invoiceId,
        decimal amount,
        string paymentMethod,
        string? transactionReference = null)
    {
        if (amount <= 0)
            throw new InvalidOperationException("Payment amount must be greater than zero.");

        var method = paymentMethod.Trim();

        if (!new[] { "Cash", "UPI", "Card", "Online" }
            .Contains(method, StringComparer.OrdinalIgnoreCase))
            throw new InvalidOperationException("Invalid payment method.");

        await using var transaction = await _db.Database.BeginTransactionAsync();

        var invoice = await _db.Invoices
            .FirstOrDefaultAsync(i => i.InvoiceId == invoiceId);

        if (invoice == null)
            throw new InvalidOperationException("Invoice not found.");

        if (invoice.Status == "Cancelled")
            throw new InvalidOperationException("Cannot pay a cancelled invoice.");

        var paidAmount = await _db.Payments
            .Where(p => p.InvoiceId == invoiceId && p.PaymentStatus == "Successful")
            .SumAsync(p => (decimal?)p.Amount) ?? 0;

        var balance = invoice.TotalAmount - paidAmount;

        if (amount > balance)
            throw new InvalidOperationException($"Payment exceeds the outstanding balance of {balance:C}.");

        var payment = new Payment
        {
            InvoiceId = invoiceId,
            Amount = amount,
            PaymentMethod = method,
            PaymentStatus = "Successful",
            TransactionReference = transactionReference,
            PaymentDate = DateTime.UtcNow
        };

        _db.Payments.Add(payment);

        invoice.Status = amount == balance ? "Paid" : "PartiallyPaid";

        await _db.SaveChangesAsync();
        await transaction.CommitAsync();

        return payment;
    }
}
