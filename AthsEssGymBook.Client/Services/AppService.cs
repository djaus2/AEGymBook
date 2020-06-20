using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Text;
using AthsEssGymBook.Shared;

namespace AthsEssGymBook.Client.Services
{

    public class AppData
    {
        public RegisterInitialParameters registerInitialParameters { get; set; }

        public AppData()
        {
            registerInitialParameters = new RegisterInitialParameters();
        }
    }
}
