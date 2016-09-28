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
using Kodestruct.Common.CalculationLogger;
using Kodestruct.Common.CalculationLogger.Interfaces;

namespace Kodestruct.Analysis.BeamForces.Simple
{

    public class ConcentratedLoadAtAnyPoint : ISingleLoadCaseBeam, ISingleLoadCaseDeflectionBeam
        {
            BeamSimple beam;
           double P, a,b, L;
           const string CASE = "C1A_2";
           bool ShearForcesCalculated;

           public ConcentratedLoadAtAnyPoint(BeamSimple beam, double P, double a)
	    {
                this.beam = beam;
                L = beam.Length;
                this.P = P;
                if (a>L)
                {
                    throw new LoadLocationParametersException(L, "a");
                }
                this.a = a;
                this.b = L - a;
                ShearForcesCalculated = false;
	    }


        public double Moment( double X)
        {
            beam.EvaluateX(X);
            double M;
            if (X<=a)
            {
                M = P * b * X / L;
                   BeamEntryFactory.CreateEntry("Mx", M, BeamTemplateType.Mx, 1,
                    new Dictionary<string, double>()
                        {
                           {"L",L },
                           {"X",X },
                           {"P",P},
                           {"b",b }
                         }, CASE, beam);
            }
            else
            {
                M = P * a * (L-X) / L;
                   BeamEntryFactory.CreateEntry("Mx", M, BeamTemplateType.Mx, 2,
                    new Dictionary<string, double>()
                        {
                           {"L",L },
                           {"X",X },
                           {"P",P},
                           {"a",a }
                         }, CASE, beam);
            }
            return M;
        }

        public double Shear( double X)
        {

            beam.EvaluateX(X);

            double V;

            if (X<a)
            {
                V = GetShearLeft();
                    BeamEntryFactory.CreateEntry("Vx", V, BeamTemplateType.Vx, 1,
                    new Dictionary<string, double>()
                        {
                           {"L",L },
                           {"X",X },
                           {"P",P},
                           {"b",b }
                         }, CASE, beam);
            }
            else if (X>a)
            {
                V = GetShearRight();
                BeamEntryFactory.CreateEntry("Vx", V, BeamTemplateType.Vx, 2,
                new Dictionary<string, double>()
                        {
                           {"L",L },
                           {"X",X },
                           {"P",P},
                           {"a",a }
                         }, CASE, beam);
            }
            else
            {
                if (a>b)
                {
                    V = GetShearRight();
                    BeamEntryFactory.CreateEntry("Vx", V, BeamTemplateType.Vx, 2,
                    new Dictionary<string, double>()
                        {
                           {"L",L },
                           {"X",X },
                           {"P",P},
                           {"a",a }
                         }, CASE, beam);
                }
                else // at the point of load
                {
                    V = GetShearLeft();
                    BeamEntryFactory.CreateEntry("Vx", V, BeamTemplateType.Vx, 1,
                    new Dictionary<string, double>()
                        {
                           {"L",L },
                           {"X",X },
                           {"P",P},
                           {"b",b }
                         }, CASE, beam);
                }
            }

            return V;
        }

        private double GetShearRight()
        {
            double V = P * a / L;
            
             return V;
        }

        private double GetShearLeft()
        {
            double V = P * b / L;
   
            return V;
        }

        public ForceDataPoint MomentMax()
        {
            double M;

            if (P > 0.0)
            {
                M = P * a * b / L;

                   BeamEntryFactory.CreateEntry("Mx", M, BeamTemplateType.Mmax, 1,
                    new Dictionary<string, double>()
                        {
                           {"L",L },
                           {"X",a },
                           {"P",P},
                           {"a",a },
                           {"b",b }
                         }, CASE, beam, true);
            }
            else
            {
                M = 0.0;
                    BeamEntryFactory.CreateEntry("Mx", M, BeamTemplateType.M_zero, 0,
                    null, CASE, beam, true);

            }
            return new ForceDataPoint(a,M);
        }

        public ForceDataPoint MomentMin()
        {
            double M;

            if (P > 0.0)
            {

                M = 0.0;
                    BeamEntryFactory.CreateEntry("Mx", M, BeamTemplateType.M_zero, 0,
                    null, CASE, beam, false,true);
             }
            else
            {

                M = P * a * b / L;
                    BeamEntryFactory.CreateEntry("Mx", M, BeamTemplateType.Mmax, 1,
                    new Dictionary<string, double>()
                        {
                           {"L",L },
                           {"X",a },
                           {"P",P},
                           {"a",a },
                           {"b",b }
                         }, CASE, beam, false, true);
            }
            return new ForceDataPoint(a, M);
        }

        public ForceDataPoint ShearMax()
        {
            double V;
            if (a > b)
            {
                 V = GetShearRight();
                    BeamEntryFactory.CreateEntry("Vx", V, BeamTemplateType.Vmax, 2,
                    new Dictionary<string, double>()
                        {
                           {"L",L },
                           {"X",a },
                           {"P",P},
                           {"a",a },
                         }, CASE, beam,true);
            }
            else
            {
                V = GetShearLeft();
                BeamEntryFactory.CreateEntry("Vx", V, BeamTemplateType.Vmax, 1,
                    new Dictionary<string, double>()
                        {
                           {"L",L },
                           {"X",a },
                           {"P",P},
                           {"b",b },
                         }, CASE, beam, true);
            }

            return new ForceDataPoint(a, V);
        }

        public double MaximumDeflection()
        {
            double E = beam.ModulusOfElasticity;
            double I = beam.MomentOfInertia;

            double delta = ((P * a * b * (a + 2 * b) * Math.Sqrt(3 * a * (a + 2 * b))) / (27 * E * I * L));

            BeamEntryFactory.CreateEntry("delta", delta, BeamTemplateType.deltaMax, 1,
            new Dictionary<string, double>()
                                    {
                                       {"b",b },
                                       {"P",P},
                                       {"a",a },
                                       {"E",E},
                                       {"I",I },
                                       {"L",L}
                                     }, CASE, beam);

            return delta;
        }

        public double Deflection(double X)
        {
            double delta;
            int CaseId;
            double E = beam.ModulusOfElasticity;
            double I = beam.MomentOfInertia;

            if (X<=a)
	        {
		        delta =(P*b*X)/(6*E*I*L)*(Math.Pow(L,2.0)-Math.Pow(b,2.0)-Math.Pow(X,2.0) );
                CaseId = 1;
	        }
            else
	        {
                delta = (P*a*(L-X))/(6*E*I*L)*(Math.Pow(L,2.0)-Math.Pow(a,2.0)-Math.Pow(L-X,2.0) );
                CaseId = 2;
	        }

            BeamEntryFactory.CreateEntry("delta", delta, BeamTemplateType.delta, CaseId,
            new Dictionary<string, double>()
                                    {
                                       {"X",X },
                                       {"P",P},
                                       {"E",E},
                                       {"I",I },
                                       {"a",a},
                                       {"b",b },
                                       {"L",L}
                                     }, CASE, beam);

            return delta;

           
        }

        }
    
}
