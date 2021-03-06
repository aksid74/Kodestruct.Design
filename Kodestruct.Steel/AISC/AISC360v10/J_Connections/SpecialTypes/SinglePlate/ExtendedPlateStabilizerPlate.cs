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
using Kodestruct.Common.Entities;

namespace Kodestruct.Steel.AISC.AISC360v10.Connections
{
    public partial class ExtendedSinglePlate : AnalyticalElement
    {
        public double GetShearStrengthWithoutStabilizerPlate(double d_pl, double t_p, double a_bolts, double F_y)
        {
            double R_n = 1500*Math.PI*(d_pl*Math.Pow(t_p,3))/Math.Pow(a_bolts,2); //Manual equation 10-6
            //per Thornton Fortney 1500 factor has units of ksi
            return 0.9 * R_n;
        }
    }
}
