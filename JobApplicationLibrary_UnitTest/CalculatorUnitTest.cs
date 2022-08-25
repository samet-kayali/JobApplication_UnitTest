using System;
using NUnit.Framework;
using System.Collections.Generic;
using JobApplicationLibrary;

namespace JobApplicationLibrary_UnitTest
{
    public class CalculatorUnitTest
    {
        //Brüt maaşı 5000 TL olan kişinin işverene toplam maaliyetini hesaplayan test.
        [Test]
        public void Calculator5000TL_MaaliyetHesapla()
        {
            //Arrange
            var hesapla = new Calculator();

            //Action
            var maaliyet = hesapla.CalculateJobCost(
                5000,
                1200,
                150);

            //Assert
            Assert.That(maaliyet, Is.EqualTo(6350));
        }


        //Brüt maaşı 7500 TL olan kişinin işverene toplam maaliyetini hesaplayan test.
        [Test]
        public void Calculator7500TL_MaaliyetHesapla()
        {
            //Arrange
            var hesapla = new Calculator();

            //Action
            var maaliyet = hesapla.CalculateJobCost(
                7500,
                1800,
                225);

            //Assert
            Assert.That(maaliyet, Is.EqualTo(9525));
        }


        //Brüt maaşı 8338 TL olan kişinin işverene toplam maaliyetini hesaplayan test.
        [Test]
        public void Calculator8338TL_MaaliyetHesapla()
        {
            //Arrange
            var hesapla = new Calculator();

            //Action
            var maaliyet = hesapla.CalculateJobCost(
                8338,
                2001.12,
                250.14);

            //Assert
            Assert.That(maaliyet, Is.EqualTo(10589.259).Within(0.009));
        }


        //Brüt maaşı 20411 TL olan kişinin işverene toplam maaliyeti toplamda 255921,97 TL olması gerekirken,
        //yüzde 5 tolerans ile 25500 TL içinde barındırmayı hesaplayan test.
        [Test]
        public void Calculator20411TL_MaaliyetHesapla()
        {
            //Arrange
            var hesapla = new Calculator();

            //Action
            var maaliyet = hesapla.CalculateJobCost(
                20411,
                4898.64,
                612.33);

            //Assert
            Assert.That(maaliyet, Is.EqualTo(25500).Within(5).Percent);
        }


        //TestCase ile farklı parametreler ile aynı test üzerinden hesaplama yapan test.
        [TestCase(5000, 1200, 150, 6350)]
        [TestCase(7500, 1800, 225, 9525)]
        [TestCase(10000, 2400, 300, 12700)]
        [TestCase(12000, 2880, 360, 15240)]
        [TestCase(15000, 3600, 450, 19050)]

        public void CalculatorMultiple_MaaliyetHesaplama(
            double net_ucret,
            double sigorta_maaliyet,
            double issizlik_fonu_maaliyet,
            double Toplam_Maaliyet)
        {
            var maaliyet = new Calculator().CalculateJobCost(
                net_ucret,
                sigorta_maaliyet,
                issizlik_fonu_maaliyet);

            //Assert
            Assert.That(maaliyet, Is.EqualTo(Toplam_Maaliyet));
        }
    }
}