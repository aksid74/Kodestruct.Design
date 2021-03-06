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
using Kodestruct.Common.Entities;
using Kodestruct.Concrete.ACI;
using Kodestruct.Concrete.ACI.Entities;

namespace Kodestruct.Concrete.ACI318_14
{
    /// <summary>
    ///  This class encpsulates sectional shear provisions per ACI.
    /// </summary>
    public partial class ConcreteSectionOneWayShearNonPrestressed : AnalyticalElement
    {

        public ConcreteSectionOneWayShearNonPrestressed(double d, IConcreteSection Section)

        {
            this.d = d;
            this.Section = Section;
            this.b_w = Section.b_w;
        }



                double d; 
                double b_w; 
                
                double A_g; 
                double N_u;
                double rho_w;


                

                private IConcreteSection  section;

                public IConcreteSection  Section
                {
                    get { return section; }
                    set { section = value; }
                }
                



    }
}
