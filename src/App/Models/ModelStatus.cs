namespace App.Models
{
    //Honestly, there is no drive from the top layer to require this resolution
    //I'd be happy with just Valid/Invalid
    public enum ModelStatus
    {
        Valid,
        InvalidDateOfBirth,
        InvalidEmail,
        InvalidName
    }
}