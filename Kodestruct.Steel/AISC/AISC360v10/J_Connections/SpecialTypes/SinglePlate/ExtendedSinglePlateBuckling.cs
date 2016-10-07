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
using System.Threading.Tasks;
using Kodestruct.Common.CalculationLogger;
using Kodestruct.Common.Entities;
using Kodestruct.Common.Section.SectionTypes;
using Kodestruct.Steel.AISC.AISC360v10.Connections.AffectedMembers;
using Kodestruct.Steel.AISC.SteelEntities.Materials;

namespace Kodestruct.Steel.AISC.AISC360v10.Connections
{
    public partial class ExtendedSinglePlate : AnalyticalElement 
    {
        public double GetExtendedSinglePlateFlexuralBucklingStrength(double F_y, double t_pl, double d_pl, double L_pl)
        {
            SectionRectangular r = new SectionRectangular(t_pl, d_pl);
            SteelMaterial mat = new SteelMaterial(F_y);
            CalcLog log = new CalcLog();

            AffectedElementInFlexure flexuralElement = new AffectedElementInFlexure(r,mat,log);
            return flexuralElement.GetPlateFlexuralBucklingStrength(L_pl);
        }
    }
}