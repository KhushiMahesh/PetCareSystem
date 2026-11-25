namespace PetCareSystem.Web.Helpers;

public static class PetSpeciesIllustration
{
    public static string GetImagePath(string? speciesName)
    {
        var fileName = speciesName?.Trim().ToLowerInvariant() switch
        {
            "dog" => "dog",
            "cat" => "cat",
            "rabbit" => "rabbit",
            "guinea pig" => "guinea-pig",
            "hamster" => "hamster",
            "cattle" => "cattle",
            "buffalo" => "buffalo",
            "sheep" => "sheep",
            "goat" => "goat",
            "pig" => "pig",
            "horse" => "horse",
            "donkey" => "donkey",
            "chicken" => "chicken",
            "duck" => "duck",
            "goose" => "goose",
            "turkey" => "turkey",
            "parrot" => "parrot",
            "pigeon" => "pigeon",
            _ => "dog"
        };

        return $"/images/animals/{fileName}.png";
    }
}