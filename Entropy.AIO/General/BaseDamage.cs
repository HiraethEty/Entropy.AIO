using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entropy.AIO.General
{
	using SDK.Spells;

	abstract class BaseDamage
	{
		public virtual float QDamage(AIBaseClient target) => 0f;
		public virtual float WDamage(AIBaseClient target) => 0f;
		public virtual float EDamage(AIBaseClient target) => 0f;
		public virtual float RDamage(AIBaseClient target) => 0f;
	}
}
