using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concretes.Enums
{
    public enum OpportunityStage
    {
        Qualification = 10, // Probability: 10%
        NeedsAnalysis = 25, // Probability: 25%
        Proposal = 50,      // Probability: 50%
        Negotiation = 75, // Probability: 75%
        ClosedWon = 100,   // Probability: 100%
        ClosedLost = 0 // Probability: 0%
    }

    public enum OpportunityStatus
    {
        Open,
        Won,
        Lost
    }
}
