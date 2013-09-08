using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrtajMeUtil;
using CrtajMeModel;

namespace CrtajMeControllers
{
    public class ResultsController
    {


        /// <summary>
        /// Dohvacaju se sva imena skupova
        /// </summary>
        /// <returns>Lista imena skupova</returns>
        public List<string> GetAllSkupovi()
        {
            return SkupRepos.Instance().ListSkupoviByName();
        }


        /// <summary>
        /// Odstranjuje se forma iz subjecta
        /// </summary>
        /// <param name="frm"></param>
        public void RemoveForm(IViewResults frm)
        {
            SkupRepos.Instance().removeObserver(frm);
            PrikazRepos.Instance().removeObserver(frm);
        }

        public string GetTresholdOfSkup(IViewResults frm, string name)
        {
            try
            {
                return SkupRepos.Instance().GetSkupByName(name).Treshold.ToString();
            }
            catch (CrtajMeException e)
            {
                frm.ShowErrorMessage(e.getMsg());
            }
            return "-";
        }

        public string GetTypeOfSkup(IViewResults frm, string name)
        {
            try
            {
                return SkupRepos.Instance().GetSkupByName(name).Type;
            }
            catch (CrtajMeException e)
            {
                frm.ShowErrorMessage(e.getMsg());
            }
            return "-";
        }

        public string GetAccept(IViewResults frm, string name)
        {
            try
            {
                bool acc = SkupRepos.Instance().GetSkupByName(name).Accept;
                if (acc == true)
                    return "DA";
                else
                    return "NE";
            }
            catch (CrtajMeException e)
            {
                frm.ShowErrorMessage(e.getMsg());
            }
            return "-";
        }

        public string GetAverageGrade(IViewResults frm, string name)
        {
            try
            {
                return SkupRepos.Instance().GetSkupByName(name).AverageGrade.ToString();
            }
            catch (CrtajMeException e)
            {
                frm.ShowErrorMessage(e.getMsg());
            }
            return "-";
        }

        public string GetTypeOfPrikaz(IViewResults frm, string name)
        {
            try
            {
                return PrikazRepos.Instance().GetPrikazByName(name).Type;
            }
            catch (CrtajMeException e)
            {
                frm.ShowErrorMessage(e.getMsg());
            }
            return "-";
        }

        public void InsertPrikazInSKup(IViewResults frm, string prikazName, string skupName)
        {
            try
            {
                Prikaz p = PrikazRepos.Instance().GetPrikazByName(prikazName);
                Skup s = SkupRepos.Instance().GetSkupByName(skupName);
                s.AddPrikaz(p);
                frm.ShowDetails();
            }
            catch (CrtajMeException e)
            {
                frm.ShowErrorMessage(e.getMsg());
            }
        }

        public List<string> GetAllPrikazi()
        {
            return PrikazRepos.Instance().ListPrikazByName();
        }

        public List<string> GetPrikaziInSkup(IViewResults frm, string name)
        {
            List<string> ret = new List<string>();
            try
            {
                ret = SkupRepos.Instance().GetSkupByName(name).GetListNameOfPrikaz();
                return ret;
            }
            catch (CrtajMeException e)
            {
                frm.ShowErrorMessage(e.getMsg());
            }
            return ret;
        }

        public void RemovePrikazFromSkup(IViewResults frm, string prikazName, string skupName)
        {
            try
            {
                Skup s = SkupRepos.Instance().GetSkupByName(skupName);
                Prikaz p = PrikazRepos.Instance().GetPrikazByName(prikazName);
                s.DeletePrikaz(p);
                frm.ShowDetails();
            }
            catch(CrtajMeException e)
            {
                frm.ShowErrorMessage(e.getMsg());
            }
        }
    }
}
