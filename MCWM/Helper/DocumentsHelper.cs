using DirecLayer;
using DomainLayer.SAP_DATABASE;
using MCWM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCWM.Helper
{
    public class DocumentsHelper
    {
        public void DocumentHeader(DocumentHeaderModel model,
                                    ORDR document)
        {
            if (Validation.isNull(model.CardCode))
            {
                document.CardCode = model.CardCode;
            }

            if (Validation.isNull(model.CardName))
            {
                document.CardName = model.CardName;
            }

            if (Validation.isNull(model.DocDate == null ? "" : model.DocDate.ToString()))
            {
                document.DocDate = model.DocDate.GetValueOrDefault(DateTime.Now);
            }

            if (Validation.isNull(model.DocDueDate == null ? "" : model.DocDueDate.ToString()))
            {
                document.DocDueDate = model.DocDueDate.GetValueOrDefault(DateTime.Now);
            }

            if (Validation.isNull(model.TaxDate == null ? "" : model.TaxDate.ToString()))
            {
                document.TaxDate = model.TaxDate.GetValueOrDefault(DateTime.Now);
            }

            if (Validation.isNull(model.Project))
            {
                document.Project = model.Project;
            }
            //return document;
        }
    }
}
