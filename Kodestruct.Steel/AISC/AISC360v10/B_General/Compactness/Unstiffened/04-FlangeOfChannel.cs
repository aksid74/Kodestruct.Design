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
using Kodestruct.Common.Entities; 
using Kodestruct.Common.Section.Interfaces; 
using Kodestruct.Steel.AISC.Interfaces;
using Kodestruct.Steel.AISC.AISC360v10.General.Compactness;
 
 
 

namespace Kodestruct.Steel.AISC.AISC360v10.General.Compactness
{

    public class FlangeOfChannel: FlangeOfRolledIShape
    {
        public FlangeOfChannel(ISteelMaterial Material, double Overhang, double Thickness)
            :base(Material, Overhang,Thickness)
        {

        }

        public FlangeOfChannel(ISteelMaterial Material, ISectionChannel s, ElementLocation location)
            :base(Material)
        {
            double bf=0;
            double tf=0;

            switch (location)
            {
                case ElementLocation.Top:
                    bf = s.b_f;
                    tf = s.t_f;
                    break;
                case ElementLocation.Bottom:
                    bf = s.b_f;
                    tf = s.t_f;
                    break;
                default:
                    throw new Exception("Invalid location is specified for Channel flange");
            }

            base.Overhang = bf;
            base.Thickness = tf;
        }
    }
}
