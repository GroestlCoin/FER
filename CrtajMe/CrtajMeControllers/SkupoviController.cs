using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrtajMeModel;

namespace CrtajMeControllers
{
    public class SkupoviController
    {
        /// <summary>
        /// Dodaje se novi skup u sustav
        /// </summary>
        /// <param name="frm">Sucelje za formu</param>
        public void AddNewSkup(INoviSkup frm)
        {
            frm.LoadPrikazTypes(PrikazRepos.Instance().GetListaTipova());

            if (frm.outerShow() == true)
            {
                try
                {
                    string skupName = frm.GetSkupName();
                    double skupTreshold = frm.GetSkupTreshold();
                    string skupType = frm.GetSkupType();
                    if (skupName != "" && skupType != "")
                    {
                        Skup noviSkup = CrtajMeModel.Factories.SkupFactory.CreateNewSkup(skupName, skupType, skupTreshold);
                        SkupRepos.Instance().AddSkup(noviSkup);
                    }
                    else
                    {
                        frm.ShowErrorMessage("Polja ne smiju biti prazna!");
                    }
                }
                catch (CrtajMeException e)
                {
                    frm.ShowErrorMessage(e.getMsg());
                }
            }
        }

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
        public void RemoveForm(IViewSkup frm)
        {
            SkupRepos.Instance().removeObserver(frm);
        }

        /// <summary>
        /// Izbacuje se skup iz repozitorija
        /// </summary>
        /// <param name="name"></param>
        public void RemoveSkup(IViewSkup frm, string name)
        {
            try
            {
                SkupRepos.Instance().RemoveSkup(name);
            }
            catch (CrtajMeException e)
            {
                frm.ShowErrorMessage(e.getMsg());
            }
        }

        public string GetTresholdOfSkup(IViewSkup frm, string name)
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

        public string GetTypeOfSkup(IViewSkup frm, string name)
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

    }
}
