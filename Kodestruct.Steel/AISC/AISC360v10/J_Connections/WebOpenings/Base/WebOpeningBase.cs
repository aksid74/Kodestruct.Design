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
using Kodestruct.Common.Section.Interfaces;
using Kodestruct.Common.Section.SectionTypes;
using Kodestruct.Steel.AISC.SteelEntities.Sections;

namespace Kodestruct.Steel.AISC.AISC360v10.Connections.WebOpenings
{
    public abstract class WebOpeningBase : IWebOpening
    {
        /// <summary>
        /// Base class for all web openings
        /// </summary>
        /// <param name="Section">Steel Section</param>
        /// <param name="a_o">Length of opening</param>
        /// <param name="h_o">Depth of opening</param>
        /// <param name="e">Eccentricity of opening (positive up)</param>
        /// <param name="F_y">Steel shape yield stress</param>
        /// <param name="t_r"> Plate thickness of opening reinforcement (top or bottom)</param>
        /// <param name="b_r"> Plate width (horizontal dimension in cross-section) for reinforcement</param>
        public WebOpeningBase(ISectionI Section, double a_o, double h_o, double e, double F_y, double t_r, double b_r,
             bool IsSingleSideReinforcement, double PlateOffset, double M_u, double V_u)
        {
                this.a_o =a_o;
                this.h_o =h_o;
                this.e = e;
                this.F_y = F_y;
                this.t_r = t_r;
                this.b_r = b_r;
            this.M_u=M_u;
            this.V_u = V_u;
                SectionI sec = new SectionI(null, Section.d, Section.b_fTop, Section.t_fTop, Section.t_w);

                   this.Section = sec;

                   this.PlateOffset = PlateOffset;
                   this.IsSingleSideReinforcement = IsSingleSideReinforcement;
        }

        double M_u, V_u;
        public double PlateOffset              {get; set;}
        public bool IsSingleSideReinforcement { get; set; }

        public double t_r { get; set; }
        public double b_r { get; set; }
        public double a_o { get; set; }

        public double  h_o { get; set; }

        public double e { get; set; }
        public double F_y { get; set; }

        private double _A_r;

        public double A_r
        {
            get {
                _A_r = GetA_r();
                return _A_r; }
            set { _A_r = value; }
        }

        private double GetA_r()
        {
            double ReinforcementArea;

            if (IsSingleSideReinforcement == true)
            {
                ReinforcementArea = 2.0 * t_r * b_r; 
            }
            else
            {
                ReinforcementArea = t_r * b_r;
            }
            return ReinforcementArea;
        }

        private double _P_r;

        public double P_r
        {
            get {
                _P_r = GetP_r();
                return _P_r; }
            set { _P_r = value; }
        }

        private double GetP_r()
        {
            double P_r1 = A_r * F_y;
            double P_r2 = F_y * Section.t_w * a_o / (2.0 * Math.Sqrt(3.0));
            return Math.Min(P_r1, P_r2);
        }
        
        
        public double s_t
        {
            get {
                double d = Section.d;
                double s_t = d / 2.0 - (e + h_o / 2.0);
                return s_t;
            }
        }

        public double s_b
        {
            get
            {
                double d = Section.d;
                double s_b = d / 2.0-(h_o / 2.0 - e);
                return s_b;
            }
        }
        public SectionI Section { get; set; }

        public  double GetShearStrength()
        {
            CheckProportions();
            double d = Section.d;

            if (IsSingleSideReinforcement == true)
            {
                if (V_u == 0)
                {
                    throw new Exception("Provide non-zero values of M_u and V_u");
                }
                else
                {
                    if (M_u/(V_u*d)>20.0)
                    {// 3-36
                        throw new Exception("M_u/(V_u*d) must be less than 20 for single-side reinforcement");
                    }
                }
            }
            double mu_Top = Get_mu_Top();
            double nu_Top = Get_nu_Top();

            double mu_Bottom = Get_mu_Bottom();
            double nu_Bottom = Get_nu_Bottom();

            double alphaTop = Get_alphaTop();
            double alphaBottom = Get_alphaBottom();


            double V_pt = GetV_pt();
            double V_pb = GetV_pb();

            double V_mt = GetV_mt(V_pt,alphaTop);
            double V_mb = V_pb * alphaBottom;

            double V_m = GetV_m(V_mt,V_mb,mu_Top,nu_Top);
            double phi = Get_phi();
            double phiV_n = phi * V_m;
            return phiV_n;
        }

        public abstract double Get_phi();


        protected double GetV_bar_p()
        {
            //TODO: Adjust this to correct for web slenderness
            double V_bar_p = this.Section.d * this.Section.t_w * 0.6 * F_y;
            return V_bar_p;
        }

        protected virtual double GetV_m(double V_mt, double V_mb, double mu, double nu)
        {
            //this method is overriden  for composite beam


            //From Table 4.5
            double V_Bar_p = GetV_bar_p();
            double V_mMax = 2.0 / 3.0 * V_Bar_p;

            double V_m = V_mt + V_mb;
            V_m = V_m > V_mMax ? V_mMax : V_m;
            return V_m;
        }

        protected double GetV_pb()
        {
            double V_pb = GetV_p(s_b);
            return V_pb;
        }

        protected double GetV_pt()
        {
            double V_pt = GetV_p(s_t);
            return V_pt;
        }
        private double GetV_p(double s)
        {
             double t_w = Section.t_w;

            double V_p = F_y * t_w * s / Math.Sqrt(3.0);

            return V_p;
        }

        protected virtual double GetV_mt(double V_pt, double alphaTop)
        {
            double V_mt = V_pt * alphaTop;
            return V_mt;
        }

        protected abstract double Get_alphaTop();
        //{
        //    double alphaTop = Get_alpha(mu, nu);
        //    return alphaTop;
        //}

        protected virtual double Get_alphaBottom()
        {
            double mu = Get_mu_Bottom();
            double nu = Get_nu_Bottom();
            double alphaBottom = Get_alpha(mu, nu);
            return alphaBottom;
        }


        protected double Get_alpha(double mu, double nu)
        {
            double alpha  = ((Math.Sqrt(6.0) + mu) / (nu + Math.Sqrt(3.0)));
            return alpha;
        }



        public abstract double Get_nu_Top();
        
        public abstract double Get_mu_Top();
        
        public abstract double Get_nu_Bottom();
        
        public abstract double Get_mu_Bottom();

        //public abstract double GetFlexuralStrength();

        protected void CheckProportions()
        {
            CheckFlangeAndReinforcementSlenderness();
            CheckWeb();
            CheckTeeSlendernessSingleSided();
        }

        private void CheckTeeSlendernessSingleSided()
        {
            if (IsSingleSideReinforcement == true)
            {
                //3-35
                double SlendernessMax = 140*Math.Sqrt(F_y);
                if (s_t / Section.t_w > SlendernessMax || s_b / Section.t_w > SlendernessMax)
                {
                    throw new Exception("s/t_w ratio for at least one of the tees exceeds maximum limit");
                }
            }
            
        }

        private void CheckWeb()
        {
            double slendernessWeb= (this.Section.d - 2.0* this.Section.t_f)/this.Section.t_w;

            double SlendernessMax = 520.0 * Math.Sqrt(Math.Min(F_y, 65.0));
            if (slendernessWeb> SlendernessMax )
            {
                throw new Exception("Web is too slender, revise.");
            }
            CheckOpeningAspectRatio();
        }

       protected abstract void  CheckOpeningAspectRatio();

        private void CheckFlangeAndReinforcementSlenderness()
        {
            double slendernessFlange = this.Section.b_f / (2.0 * this.Section.t_f);
            double slendernessReinforcement = this.b_r / t_r;

            double SlendernessMax = 65.0 * Math.Sqrt(Math.Min(F_y, 65.0));
            if (slendernessFlange> SlendernessMax || slendernessReinforcement>SlendernessMax)
            {
                throw new Exception("Flange or reinforcement plate are too slender, revise.");
            }
        }

        
        
    }
}
