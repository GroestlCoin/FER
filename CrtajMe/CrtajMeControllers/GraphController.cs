using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrtajMeModel;

namespace CrtajMeControllers
{
    public class GraphController
    {
        public void SavePrikaz(IVisualView frm, string prikazName, double ocjena)
        {
            try
            {
                Prikaz p = PrikazRepos.Instance().GetPrikazByName(prikazName);
                p.Grade = ocjena;
            }
            catch(CrtajMeException e)
            {
                frm.ShowErrorMessage(e.getMsg());
            }
        }
    }
}
