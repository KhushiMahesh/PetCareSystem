window.printInvoice = function (elementId) {
    const invoice = document.getElementById(elementId);

    if (!invoice) {
        alert("Invoice not found.");
        return;
    }

    const printArea = document.createElement("div");
    printArea.id = "petcare-print-area";

    const clone = invoice.cloneNode(true);

    clone.removeAttribute("id");
    clone.querySelectorAll(".no-print").forEach(el => el.remove());

    printArea.innerHTML = `
        <div class="invoice-header">
            <div>
                <h1>🐾 Pawberry Animal Hospital</h1>
                <p>Veterinary Care & Billing</p>
            </div>
            <div class="invoice-label">INVOICE</div>
        </div>
        <hr>
    `;

    printArea.appendChild(clone);

    const style = document.createElement("style");
    style.id = "petcare-print-css";

    style.textContent = `
        @page {
            size: A4 portrait;
            margin: 14mm;
        }

        #petcare-print-area {
            display: none;
        }

        @media print {
            body > * {
                display: none !important;
            }

            body > #petcare-print-area {
                display: block !important;
                width: 100% !important;
                font-family: Arial, sans-serif !important;
                color: #222 !important;
                font-size: 11pt !important;
            }

            #petcare-print-area .invoice-header {
                display: flex;
                justify-content: space-between;
                align-items: center;
                margin-bottom: 18px;
            }

            #petcare-print-area .invoice-header h1 {
                font-size: 23pt;
                margin: 0;
                font-weight: bold;
            }

            #petcare-print-area .invoice-header p {
                margin: 4px 0;
                color: #555;
            }

            #petcare-print-area .invoice-label {
                font-size: 20pt;
                font-weight: bold;
                letter-spacing: 2px;
            }

            #petcare-print-area .card {
                border: none !important;
                box-shadow: none !important;
                width: 100% !important;
            }

            #petcare-print-area .card-body {
                padding: 0 !important;
            }

            #petcare-print-area h5 {
                font-size: 17pt !important;
                margin-bottom: 10px !important;
            }

            #petcare-print-area h6 {
                font-size: 12pt !important;
                font-weight: bold !important;
                margin: 20px 0 8px !important;
            }

            #petcare-print-area .row {
                display: flex !important;
                flex-wrap: nowrap !important;
                width: 100% !important;
            }

            #petcare-print-area .col-md-3 {
                flex: 1 !important;
                width: 33.33% !important;
            }

            #petcare-print-area table {
                width: 100% !important;
                border-collapse: collapse !important;
                table-layout: fixed !important;
                margin-top: 8px !important;
            }

            #petcare-print-area th,
            #petcare-print-area td {
                border: 1px solid #bbb !important;
                padding: 10px !important;
                text-align: left !important;
                font-size: 10pt !important;
            }

            #petcare-print-area th {
                background: #f0f2f5 !important;
            }

            #petcare-print-area .border.rounded {
                border: 1px solid #ccc !important;
                padding: 10px !important;
                margin-bottom: 6px !important;
            }

            #petcare-print-area hr {
                border-top: 1px solid #aaa !important;
                margin: 12px 0 !important;
            }

            #petcare-print-area .badge {
                border: 1px solid #aaa !important;
                padding: 5px 10px !important;
            }

            #petcare-print-area,
            #petcare-print-area table,
            #petcare-print-area tr {
                break-inside: avoid !important;
                page-break-inside: avoid !important;
            }
        }
    `;

    document.head.appendChild(style);
    document.body.appendChild(printArea);

    window.addEventListener("afterprint", function cleanup() {
        printArea.remove();
        style.remove();
        window.removeEventListener("afterprint", cleanup);
    });

    window.print();
};
