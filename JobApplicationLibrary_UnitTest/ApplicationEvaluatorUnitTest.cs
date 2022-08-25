using NUnit.Framework;
using Moq;
using JobApplicationLibrary;
using JobApplicationLibrary.Services;

namespace JobApplicationLibrary_UnitTest
{
    public class ApplicationEvaluator_Tests
    {
        //Forma girilen yaşın 18 den küçük olduğu zaman AutoRejected (başvuru başarısız) yapan test.
        [Test]
        public void WithUnderAge_TransferredToAutoRejected()
        {
            //Arange
            var evaluator = new ApplicationEvaluator(null);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 16
                }
            };

            //Action
            var appRESULT = evaluator.Evaluate(form);

            //Assert
            Assert.That(appRESULT, Is.EqualTo(ApplicationResult.AutoRejected));

        }


        //Ignore metot ile yapılan testin atlanmasını sağlayan test.
        [Test]
        [Ignore("Yapılan testin atlanmasını göstermesini sağlandı.")]
        public void WithUnderAge_TransferredToHR()
        {
            //Sahte bir sınıf oluşturuldu .
            var mockValidator = new Mock<IIdentityValidator>(MockBehavior.Default);

            //IsValid ile IsAny (gönderilen parametre farketmeksizin),
            //geriye false değerini döndürüldü
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(false);
            //Arange
            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant()
                {
                    Age = 19
                }
            };

            //Action
            var appRESULT = evaluator.Evaluate(form);

            //Assert
            Assert.That(appRESULT, Is.EqualTo(ApplicationResult.TransferredToHR));

        }


        //Forma girilen programlama / teknoloji değerinin en az %25 olduğunu gösteren ve
        //AutoRejected (başvuru başarısız) testini yapar.
        [Test]
        public void WithNoStechStack_TransferredToAutoRejected()
        {
            //Sahte bir sınıf oluşturuldu .
            var mockValidator = new Mock<IIdentityValidator>();

            //IsValid ile (gönderilen parametre ile formdan gönderilen paramter aynı),
            //geriye true değerini döndürüldü
            mockValidator.Setup(i => i.IsValid("123")).Returns(true);

            //Arange
            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant() { Age = 18, IdentityNumber = "123" },
                TeachStackList = new System.Collections.Generic.List<string>()
                {
                    "Arduino"
                }

            };

            //Action
            var appRESULT = evaluator.Evaluate(form);

            //Assert
            Assert.That(appRESULT, Is.EqualTo(ApplicationResult.AutoRejected));
        }


        //Forma girilen yaşın 18 den büyük olduğu zaman ve kullandığı program/teknolojinin %75 in üstünde 
        //fakat döndürülen değer false döndürüldüğü için İK'ya gönderilen test.
        [Test]
        public void WithTechStackOver75p_TransferredToHR()
        {
            //Sahte bir sınıf oluşturuldu .
            var mockValidator = new Mock<IIdentityValidator>(MockBehavior.Default);

            //IsValid ile IsAny (gönderilen parametre farketmeksizin),
            //geriye false değerini döndürüldü
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(false);

            //Arange
            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant() { Age = 28, IdentityNumber = "" },
                TeachStackList = new System.Collections.Generic.List<string>()
                {
                    "C#", "Java", "Visual Studio","Arduino"
                },
                YearsOfExperience = 6

            };

            //Action
            var appRESULT = evaluator.Evaluate(form);

            //Assert
            Assert.That(appRESULT, Is.EqualTo(ApplicationResult.TransferredToHR));
        }


        //Forma girilen yaşın 18 den büyük olduğu ve kullandığı program/teknolojinin %50 ve
        //istenmeyen program olduğunda AutoRejected (başvuru başarısız) testini yapar.
        [Test]
        public void WithTechStackOver50p_TransferredToAutoRejected()
        {
            //Sahte bir sınıf oluşturuldu .
            var mockValidator = new Mock<IIdentityValidator>(MockBehavior.Loose);

            //IsValid ile IsAny (gönderilen parametre farketmeksizin),
            //geriye true değerini döndürüldü
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(true);

            //Arange
            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant() { Age = 28 },
                TeachStackList = new System.Collections.Generic.List<string>()
                {
                    "C#", "Java", "C+"
                },
                YearsOfExperience = 6

            };

            //Action
            var appRESULT = evaluator.Evaluate(form);

            //Assert
            Assert.That(appRESULT, Is.EqualTo(ApplicationResult.AutoRejected));
        }


        //Kimlikteki sonuç yanlış olduğu için İnsan Kaynaklarına gönderildiğinin sağlamasını yapan test
        [Test]
        public void WithNonInValidIdentity_TransferredToHR()
        {
            //Sahte bir sınıf oluşturuldu .
            var mockValidator = new Mock<IIdentityValidator>(MockBehavior.Strict);

            //IsValid ile IsAny (gönderilen parametre farketmeksizin),
            //geriye false değerini döndürüldü 
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(false);

            //Arange
            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant() { Age = 24 },
                TeachStackList = new System.Collections.Generic.List<string>()
                {
                    "C#", "Java", "C+","Arduino"
                },
                YearsOfExperience = 6
            };

            //Action
            var appRESULT = evaluator.Evaluate(form);

            //Assert
            Assert.That(appRESULT, Is.EqualTo(ApplicationResult.TransferredToHR));
        }


        //Forma girilen yaşın 18 den büyük olduğu zaman ve kullandığı program/teknolojinin %90'ın üstünde
        //ve iş tecrübsei 12 yıl olduğu için TL'e gönderilen test.
        [Test]
        public void WithTechStackOver100p_TransferredToTL()
        {
            //Sahte bir sınıf oluşturuldu .
            var mockValidator = new Mock<IIdentityValidator>();

            //IsValid ile IsAny (gönderilen parametre farketmeksizin),
            //geriye true değerini döndürüldü
            mockValidator.Setup(i => i.IsValid(It.IsAny<string>())).Returns(true);

            //Arange
            var evaluator = new ApplicationEvaluator(mockValidator.Object);
            var form = new JobApplication()
            {
                Applicant = new Applicant() { Age = 30 },
                TeachStackList = new System.Collections.Generic.List<string>()
                {
                  "C#", "Java", "Arduino ", "C++", "Visual Studio"
                },
                YearsOfExperience = 11

            };

            //Action
            var appRESULT = evaluator.Evaluate(form);

            //Assert
            Assert.That(appRESULT, Is.EqualTo(ApplicationResult.TransferredToTL));
        }

    }
}