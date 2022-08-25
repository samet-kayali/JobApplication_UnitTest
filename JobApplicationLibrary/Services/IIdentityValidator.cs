namespace JobApplicationLibrary.Services
{
    public interface IIdentityValidator
    {
        //Dış servise girilen kimliğe göre BOOL veri tipi kullanıldığı için doğruysa True değeri döndürür,
        //değilse False değeri döndürür.
        bool IsValid(string identityNumber);
    }
}