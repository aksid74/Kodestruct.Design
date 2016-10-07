#region Copyright
   /*Copyright (C) 2015 Kodestruct Inc

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
using Kodestruct.Common.Entities; 
using Kodestruct.Common.Section.Interfaces; 
using Kodestruct.Steel.AISC.Interfaces; 
 using Kodestruct.Common.CalculationLogger;
using Kodestruct.Steel.AISC.AISC360v10.General.Compactness;


namespace Kodestruct.Steel.AISC.AISC360v10.Flexure
{
    public abstract partial class FlexuralMemberIBase : FlexuralMember
    {

        internal double GetRpg(FlexuralCompressionFiberPosition compressionFiberPosition)
        {
            double Rpg = 0;

            double hc = Gethc(compressionFiberPosition);
            double tw = this.SectionI.t_w;
            double aw = Getaw(compressionFiberPosition);

            double E = Section.Material.ModulusOfElasticity;
            double Fy = Section.Material.YieldStress;

            Rpg = 1.0 - aw / (1200.0 + 300.0 * aw) * (hc / tw - 5.7 * Math.Sqrt(E / Fy)); //(F5-6)
            Rpg = Rpg > 1.0 ? 1.0 : Rpg;

            return Rpg;

        }

    }
}