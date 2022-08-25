using JobApplicationLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationLibrary
{
    public class ApplicationEvaluator
    {
        //Formdan girilen yaşın minimum 18 olarak belirlendi.
        private const int minAge = 18;

        //Bilinmesi gereken programlar / teknolojiler (formdan gelen bilgilere göre karşılaştırma yapar)
        private List<string> TechStackList = new() { "C#", "Java", "Arduino ", "C++", "Visual Studio" };

        //İş tecrübesi minimum 5 yıl olarak belirlendi.
        private const int autoAcceptedYearsOfExperience = 5;

        //Kimlik doğrulaması için  nesne türetildi.
        private readonly IIdentityValidator identityValidator;


        //interface (servis olarak kullanmak için türetildi) üzerinden parametrenin dışarıdan gönderilmesini sağlar.  
        public ApplicationEvaluator(IIdentityValidator identityValidator)
        {
            this.identityValidator = identityValidator;
        }

        public ApplicationResult Evaluate(JobApplication form)
        {
            //Form içerisinden gönderilen yaşa göre metot belirlendi.
            if (form.Applicant.Age < minAge)
                return ApplicationResult.AutoRejected;

            //Eğer formdan gelen değer doğruysa Isvalid true değer döndürür değilse false değerini döndürür.
            var validIdentity = identityValidator.IsValid(form.Applicant.IdentityNumber);

            //Kimlik doğrulaması yanlış ise İnsan Kaynaklarına formu yollayan metot belirlendi.
            if (!validIdentity)
                return ApplicationResult.TransferredToHR;

            //Bilinen programlara / teknolojilere ve tecrübeye göre metot belirlendi.
            var percent = GetTechStackRate(form.TeachStackList);
            if (percent < 25)
                return ApplicationResult.AutoRejected;

            if (percent > 75 && form.YearsOfExperience > autoAcceptedYearsOfExperience)
                return ApplicationResult.TransferredToTL;

            if (percent >= 90 && form.YearsOfExperience > autoAcceptedYearsOfExperience)
                return ApplicationResult.AutoAccepted;
            return ApplicationResult.AutoAccepted;
        }

        private int GetTechStackRate(List<string> techStacks)
        {
            //Formda girilen programlar / teklonojiler TechStackList de bulunanlara göre yüzde alındı.

            var matchedCount = techStacks.Where(i => TechStackList.Contains(i, StringComparer.OrdinalIgnoreCase))
               .Count();
            return (int)((double)matchedCount / TechStackList.Count) * 100;
        }

    }

    //Başvuruların Sonuçları
    public enum ApplicationResult
    {

        AutoRejected,
        TransferredToHR,
        TransferredToTL,
        AutoAccepted

    }
}