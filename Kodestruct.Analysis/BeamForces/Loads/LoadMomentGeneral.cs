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

namespace Kodestruct.Analysis
{
    public class LoadMomentGeneral : LoadMoment
    {
        public double Location { get; set; }
        public LoadMomentGeneral(double Mo, double Location)
            : base(new List<double>() { Mo })
        {
            this.Location = Location;
            this.Mo = Mo;
        }

        private double _Mo;

        public double Mo
        {
            get { return _Mo; }
            set
            {
                _Mo = value;
                if (Values.Count > 1)
                {
                    Values[0] = value;
                }
                else
                {
                    Values.Add(value);
                }
            }
        }
    }
}
