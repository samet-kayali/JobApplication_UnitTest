using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationLibrary
{
    public class JobApplication
    {
        //Başvuran kişinin bilgileri
        public Applicant Applicant { get; set; }

        //Başvuran kişinin sahip olduğu iş tecrübesi 
        public int YearsOfExperience { get; set; }

        //Başvuran kişinin bildiği (kullanmış) olduğu programlar || teknolojiler 
        public List<string> TeachStackList { get; set; }

    }
}
