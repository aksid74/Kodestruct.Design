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



namespace Kodestruct.Steel.AISC.AISC360v10.Flexure
{
    public abstract partial class FlexuralMemberIBase : FlexuralMember
    {

        public double GetCompressionFlangeWidthbfc(FlexuralCompressionFiberPosition compressionFiberPosition)
        {
            double bfc = 0.0;

            switch (compressionFiberPosition)
            {
                case FlexuralCompressionFiberPosition.Top:
                    bfc = Get_tfTop();
                    break;
                case FlexuralCompressionFiberPosition.Bottom:
                    bfc = Get_tfBottom();
                    break;
                default:
                    throw new Exception("Compression Flange not defined for I-shape and Channel weak axis bending");
            }


            return bfc;
        }

        public double GetCompressionFlangeThicknesstfc(FlexuralCompressionFiberPosition compressionFiberPosition)
        {
            double tfc = 0.0;
            switch (compressionFiberPosition)
            {
                case FlexuralCompressionFiberPosition.Top:
                    tfc = Get_tfTop();
                    break;
                case FlexuralCompressionFiberPosition.Bottom:
                    tfc = Get_tfBottom();
                    break;
                default:
                    throw new Exception("Compressin Flange not defined for I-shape and Channel weak axis bending");
            }
            return tfc;
        }
    }
}
