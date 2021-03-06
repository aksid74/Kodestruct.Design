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
 
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kodestruct.Loads.ASCE.ASCE7_10.DeadLoads.Components;

namespace Kodestruct.Loads.Tests.ASCE7.ASCE7_10.C03_DeadLoads
{
    [TestFixture]
    public partial class AsceComponentDeadWeightTests
    {
        [Test]
        public void ConcreteOnDeck1_5InvLWReturnsValue()
        {
            ComponentDeckWithLWFill1_5 concDeck = new ComponentDeckWithLWFill1_5(4, 3, 0);
            var deckEntry = concDeck.Weight;
            Assert.AreEqual(47, deckEntry);
        }

        [Test]
        public void ConcreteOnDeck3LWReturnsValue()
        {
            ComponentDeckWithLWFill3 concDeck = new ComponentDeckWithLWFill3(1, 3, 0);
            var deckEntry = concDeck.Weight;
            Assert.AreEqual(39, deckEntry);
        }
    }
}
