#region Copyright
   /*Copyright (C) 2015 Konstantin Udilovich

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
   */
#endregion
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodestruct.Concrete.ACI.Entities
{
    public class ResultOfShearStressDueToMoment
    {
        public double v_max { get; set; }
        public double v_min { get; set; }

        public double gamma_vx { get; set; }
        public double gamma_vy { get; set; }

        //public double J_x { get; set; }
        //public double J_y { get; set; }
        //public double theta { get; set; }

        //public ResultOfShearStressDueToMoment(double v_max, double v_min, double J_x, double J_y, double theta)
        //{
        //    this.v_max =v_max ;
        //    this.v_min =v_min ;
        //    this.J_x   =J_x   ;
        //    this.J_y   =J_y   ;
        //    this.theta = theta;
        //}

        public ResultOfShearStressDueToMoment(double v_max, double v_min, double gamma_vx, double gamma_vy)
        {
            this.v_max = v_max;
            this.v_min = v_min;
            this.gamma_vx = gamma_vx;
            this.gamma_vy = gamma_vy;
        }
    }
}
