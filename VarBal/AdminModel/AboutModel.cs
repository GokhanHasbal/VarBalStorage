using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VarBal.Models;

namespace VarBal.AdminModel
{
    public class AboutModel
    {
        public List<About> Abouts { get; set; }
        public List<OurWorker> OurWorkers { get; set; }
        public List<VizionAndMission> VizionAndMissions { get; set; }
        public List<AboutFeature> AboutFeature { get; set; }

    }
}