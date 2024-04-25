using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BundesligaDatenLader.Model
{
    public class Team
    {
        public int teamId { get; set; }
        public string teamName { get; set; }
        public string shortName { get; set; }
        public string teamIconUrl { get; set; }
        public object teamGroupName { get; set; }
    }

}
