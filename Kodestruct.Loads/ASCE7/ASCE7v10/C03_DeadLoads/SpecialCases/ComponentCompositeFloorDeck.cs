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
using System.IO;
using System.Linq;
using System.Text;
using Kodestruct.Common.Data;
using Kodestruct.Loads.Properties;

namespace Kodestruct.Loads.ASCE.ASCE7_10.DeadLoads.Components
{

    public class ComponentCompositeFloorDeck : BuildingComponentBase
    {
        public ComponentCompositeFloorDeck(int Option1, int Option2, double NumericValue)
            : base(Option1, Option2, NumericValue)
        {
            switch (Option1)
            {
             case 0:Gage=DeckGage.Gage22; break;
             case 1:Gage=DeckGage.Gage20; break;
             case 2:Gage=DeckGage.Gage18; break;
             case 3:Gage = DeckGage.Gage16; break;
             default: Gage = DeckGage.Gage16; break;
            }

            switch (Option2)
            {
                case 0: DepthType = DeckDepthType.d1_5; break;
                case 1: DepthType = DeckDepthType.d2; break;
                case 2: DepthType = DeckDepthType.d3; break;
                default: DepthType = DeckDepthType.d3; break;
            }
        }

        enum DeckDepthType
        {
            d1_5,
            d2,
            d3
        }
        enum DeckGage
        {
             Gage22,
             Gage20,
             Gage18,
             Gage16
        }

        DeckDepthType DepthType { get; set; }
        DeckGage Gage { get; set; }

        protected override void Calculate()
        {
            double q_deck = 0;

            #region Read table data

            var SampleValue = new
            {
                DepthType = "",
                ProfileType = "",
                Gage = "",
                Weight = ""
            }; // sample
            var ComponentWeightList = ListFactory.MakeList(SampleValue);

            using (StringReader reader = new StringReader(Resources.DeadLoadCompositeDeckOnly))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] Vals = line.Split(',');
                    if (Vals.Count() == 4)
                    {

                        string DepthType = (string)Vals[0];
                        string ProfileType = (string)Vals[1];
                        string Gage = (string)Vals[2];
                        string Weight = (string)Vals[3];

                        ComponentWeightList.Add(new
                        {
                            DepthType = DepthType,
                            ProfileType = ProfileType,
                            Gage = Gage,
                            Weight = Weight

                        });
                    }
                }

            }

            #endregion


            var DataValues = from weightEntry in ComponentWeightList
                             where
                                 (weightEntry.DepthType == DepthType.ToString() &&
                                 weightEntry.Gage == Gage.ToString())
                             select weightEntry;
            var ResultList = (DataValues.ToList());
            double LoadVal;

            try
            {
                if (ResultList.FirstOrDefault() != null)
                {
                    if (Double.TryParse(ResultList.FirstOrDefault().Weight, out LoadVal)) q_deck = LoadVal;
                }
            }
            catch
            {

            }

            //define strings for the report

            string DeckDepthString = null;
            switch (DepthType)
            {
                case DeckDepthType.d1_5:
                    DeckDepthString = "1 1/2";
                    break;
                case DeckDepthType.d2:
                    DeckDepthString = "2";
                    break;
                case DeckDepthType.d3:
                    DeckDepthString = "3";
                    break;
                default:
                    DeckDepthString = "3";
                    break;
            }
            string GageString = null;
            switch (Gage)
            {
                case DeckGage.Gage22:
                    GageString = "22";
                    break;
                case DeckGage.Gage20:
                    GageString = "20";
                    break;
                case DeckGage.Gage18:
                    GageString = "18";
                    break;
                case DeckGage.Gage16:
                    GageString = "16";
                    break;
                default:
                    break;
            }

            base.Weight = q_deck;
            base.Notes = string.Format
                ("{0} deep composite deck (gage {1})",
                DeckDepthString, GageString);

        }
    }
}
