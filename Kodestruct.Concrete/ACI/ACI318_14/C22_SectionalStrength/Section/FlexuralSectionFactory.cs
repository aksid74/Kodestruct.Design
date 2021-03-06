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
using Kodestruct.Common.CalculationLogger;
using Kodestruct.Common.Mathematics;
using Kodestruct.Concrete.ACI;
using Kodestruct.Concrete.ACI318_14.Materials;
using Kodestruct.Common.Section.General;
using Kodestruct.Concrete.ACI.Entities;

namespace Kodestruct.Concrete.ACI318_14
{
    public partial class FlexuralSectionFactory
    {

//        public ConcreteSectionFlexure GetRectangularSectionWithBoundaryZones(double b, double h,
//double A_sBoundary, double L_Boundary, RebarDesignation curtainBar, double s_curtain, int N_curtains, IConcreteMaterial mat, IRebarMaterial rebarMaterial,
//    ConfinementReinforcementType ConfinementReinforcementType, int NumberOfSubdivisions = 20)
//        {

//            double YTop = h / 2.0 - c_centTopBottom;
//            double YBottom = -h / 2.0 + c_centTopBottom;
//            double XLeft = -b / 2.0 + c_centLeftRight;
//            double XRight = b / 2.0 - c_centLeftRight;

//            Point2D P1 = new Point2D(XLeft, YTop);
//            Point2D P2 = new Point2D(XRight, YTop);
//            Point2D P3 = new Point2D(XRight, YBottom);
//            Point2D P4 = new Point2D(XLeft, YBottom);



//            RebarLine topLine = null;
//            RebarLine bottomLine = null;

//            RebarLine leftLine = null;
//            RebarLine rightLine = null;

//            if (NumberOfSubdivisions == 0)
//            {
//                topLine = new RebarLine(A_sTopBottom, P1, P2, rebarMaterial, false);
//                bottomLine = new RebarLine(A_sTopBottom, P3, P4, rebarMaterial, false);
//                if (A_sLeftRight > 0)
//                {
//                    leftLine = new RebarLine(A_sLeftRight, P2, P3, rebarMaterial, true);
//                    rightLine = new RebarLine(A_sLeftRight, P4, P1, rebarMaterial, true);
//                }

//            }
//            else
//            {
//                topLine = new RebarLine(A_sTopBottom, P1, P2, rebarMaterial, false, false, NumberOfSubdivisions);
//                bottomLine = new RebarLine(A_sTopBottom, P3, P4, rebarMaterial, false, false, NumberOfSubdivisions);
//                if (A_sLeftRight > 0)
//                {
//                    leftLine = new RebarLine(A_sLeftRight, P2, P3, rebarMaterial, true, false, NumberOfSubdivisions);
//                    rightLine = new RebarLine(A_sLeftRight, P4, P1, rebarMaterial, true, false, NumberOfSubdivisions);
//                }
//            }


//            List<RebarPoint> LongitudinalBars = new List<RebarPoint>();


//            if (topLine != null) LongitudinalBars.AddRange(topLine.RebarPoints);
//            if (bottomLine != null) LongitudinalBars.AddRange(bottomLine.RebarPoints);
//            if (leftLine != null) LongitudinalBars.AddRange(leftLine.RebarPoints);
//            if (rightLine != null) LongitudinalBars.AddRange(rightLine.RebarPoints);


//            CrossSectionRectangularShape section = new CrossSectionRectangularShape(mat, null, b, h);
//            CalcLog log = new CalcLog();

//            ConcreteSectionFlexure sectionFlexure = new ConcreteSectionFlexure(section, LongitudinalBars, log, ConfinementReinforcementType);
//            return sectionFlexure;
//        }

        public ConcreteSectionFlexure GetRectangularSectionFourSidesDistributed(double b, double h,
    double A_sTopBottom, double A_sLeftRight, double c_centTopBottom, double c_centLeftRight, IConcreteMaterial mat, IRebarMaterial rebarMaterial,
            ConfinementReinforcementType ConfinementReinforcementType, int NumberOfSubdivisions =0)
        {

            double YTop = h / 2.0 - c_centTopBottom;
            double YBottom = -h / 2.0 + c_centTopBottom;
            double XLeft = -b / 2.0 + c_centLeftRight;
            double XRight = b / 2.0 - c_centLeftRight;

            Point2D P1 = new Point2D(XLeft, YTop);
            Point2D P2 = new Point2D(XRight, YTop);
            Point2D P3 = new Point2D(XRight, YBottom);
            Point2D P4 = new Point2D(XLeft, YBottom);


            
            RebarLine topLine   =null;
            RebarLine bottomLine=null;

            RebarLine leftLine  =null;
            RebarLine rightLine =null;

            if (NumberOfSubdivisions == 0)
            {
                    topLine = new RebarLine(A_sTopBottom, P1, P2, rebarMaterial, false);
                    bottomLine = new RebarLine(A_sTopBottom, P3, P4, rebarMaterial, false);
                    if (A_sLeftRight>0)
                    {
                        leftLine = new RebarLine(A_sLeftRight, P2, P3, rebarMaterial, true);
                        rightLine = new RebarLine(A_sLeftRight, P4, P1, rebarMaterial, true); 
                    }

            }
            else
            {
                    topLine = new RebarLine(A_sTopBottom, P1, P2, rebarMaterial, false,   false,  NumberOfSubdivisions);
                    bottomLine = new RebarLine(A_sTopBottom, P3, P4, rebarMaterial, false,false,  NumberOfSubdivisions);
                    if (A_sLeftRight > 0)
                    {
                        leftLine = new RebarLine(A_sLeftRight, P2, P3, rebarMaterial, true, false, NumberOfSubdivisions);
                        rightLine = new RebarLine(A_sLeftRight, P4, P1, rebarMaterial, true, false, NumberOfSubdivisions);
                    }
            }


            List<RebarPoint> LongitudinalBars = new List<RebarPoint>();


            if (topLine!= null   )    LongitudinalBars.AddRange(topLine.RebarPoints);
            if (bottomLine!= null)    LongitudinalBars.AddRange(bottomLine.RebarPoints);
            if (leftLine!= null  )    LongitudinalBars.AddRange(leftLine.RebarPoints);
            if (rightLine!= null )    LongitudinalBars.AddRange(rightLine.RebarPoints);


            CrossSectionRectangularShape section = new CrossSectionRectangularShape(mat, null, b, h);
            CalcLog log = new CalcLog();

            ConcreteSectionFlexure sectionFlexure = new ConcreteSectionFlexure(section, LongitudinalBars, log, ConfinementReinforcementType);
            return sectionFlexure;
        }

       public ConcreteSectionFlexure GetNonPrestressedDoublyReinforcedRectangularSection(double b, double h, 
            double A_s1,double A_s2,double c_cntr1,double c_cntr2,
            ConcreteMaterial concreteMaterial, IRebarMaterial rebarMaterial, ConfinementReinforcementType ConfinementReinforcementType)
        {
            return GetNonPrestressedDoublyReinforcedRectangularSection(b, h, A_s1, A_s2, c_cntr1, c_cntr2, 0, 0, 0, 0, concreteMaterial, rebarMaterial, ConfinementReinforcementType);
        }


        public ConcreteSectionFlexure GetNonPrestressedDoublyReinforcedRectangularSection(double b, double h, 
            double A_s1,double A_s2,double c_cntr1,double c_cntr2, 
            double A_s_prime1,double A_s_prime2, double c_cntr_prime1, double c_cntr_prime2,
            ConcreteMaterial concrete, IRebarMaterial rebar, ConfinementReinforcementType ConfinementReinforcementType)
        {
            CrossSectionRectangularShape Section = new CrossSectionRectangularShape(concrete, null, b, h);
             List<RebarPoint> LongitudinalBars = new List<RebarPoint>();

            Rebar bottom1 = new Rebar(A_s1, rebar);
            RebarPoint pointBottom1 = new RebarPoint(bottom1, new RebarCoordinate() { X = 0, Y = -h / 2.0 + c_cntr1 });
            LongitudinalBars.Add(pointBottom1);


            if (A_s2!=0)
            {
                Rebar bottom2 = new Rebar(A_s2, rebar);
                RebarPoint pointBottom2 = new RebarPoint(bottom2, new RebarCoordinate() { X = 0, Y = -h / 2.0 + c_cntr2 });
                LongitudinalBars.Add(pointBottom2);
            }

            if (A_s_prime1 != 0)
            {
                Rebar top1 = new Rebar(A_s_prime1, rebar);
                RebarPoint pointTop1 = new RebarPoint(top1, new RebarCoordinate() { X = 0, Y = h / 2.0 - c_cntr_prime1 });
                LongitudinalBars.Add(pointTop1);
            }

            if (A_s_prime2 != 0)
            {
                Rebar top2 = new Rebar(A_s_prime2, rebar);
                RebarPoint pointTop2 = new RebarPoint(top2, new RebarCoordinate() { X = 0, Y = h / 2.0 - c_cntr_prime2 });
                LongitudinalBars.Add(pointTop2);
            }

            CalcLog log = new CalcLog();
            ConcreteSectionFlexure beam = new ConcreteSectionFlexure(Section, LongitudinalBars, log, ConfinementReinforcementType);
            return beam;
        }

        /// <summary>
        /// Concrete generic shape
        /// </summary>
        /// <param name="PolygonPoints">Points representing closed polyline describing the outline of concrete shape</param>
        /// <param name="Concrete">Concrete material</param>
        /// <param name="RebarPoints">Points representing vertical rebar. Rebar points have associated rebar material and location</param>
        /// <param name="b_w">Section width (required for shear strength calculations)</param>
        /// <param name="ConfinementReinforcementType"></param>
        /// <param name="d">Distance from tension rebar centroid to the furthermost compressed point (required for shear strength calculations)</param>
        /// <returns></returns>
       public ConcreteSectionFlexure GetGeneralSection(List<Point2D> PolygonPoints,
            IConcreteMaterial Concrete, List<RebarPoint> RebarPoints, double b_w, double d, 
           ConfinementReinforcementType ConfinementReinforcementType= ConfinementReinforcementType.NoReinforcement)
        {
            CalcLog log = new CalcLog();
            var GenericShape = new PolygonShape(PolygonPoints);
            CrossSectionGeneralShape Section = new CrossSectionGeneralShape(Concrete, null, GenericShape, b_w, d);
            ConcreteSectionFlexure beam = new ConcreteSectionFlexure(Section, RebarPoints, log,  ConfinementReinforcementType);
            return beam;
        }
    }
}
