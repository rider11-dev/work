using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.YS.Client.Evaluation
{
    public class EvaluationContext : PartyBuildingContext
    {
        public static List<Attach> upload_attaches = new List<Attach>
        {
            new Attach{att_name="开展党建工作述职评议工作汇报.docx",att_size="18.6KB"},
            new Attach{att_name="党委书记公开承诺书.docx",att_size="16.1KB"},
        };
    }
}
