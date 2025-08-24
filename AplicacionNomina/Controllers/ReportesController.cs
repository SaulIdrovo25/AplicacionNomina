using AplicacionNomina.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionNomina.Controllers
{
    public class ReportesController : Controller
    {
        // GET: Reportes
        Reportes d = new Reportes();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LeerCargos() {
            string procedimiento = "CargosUsuarios";
            DataTable[] dtArray = d.EjecutarConsulta(procedimiento);
            DataTable Tabladatos = dtArray[0];
            loadTable(Tabladatos);
            
            return View("Index");
        }
        [HttpPost]
        public ActionResult DescargarExcel()
        {
            DataTable[] tablas = d.EjecutarConsulta("CargosUsuarios");
            DataTable dt = tablas[0];
            using (var workbook = new XLWorkbook())
            {
                var hoja = workbook.Worksheets.Add("Nomina");
                hoja.Cell(1, 1).InsertTable(dt);

                using (var ms = new MemoryStream())
                {
                    workbook.SaveAs(ms);
                    ms.Position = 0;

                    return File(ms.ToArray(),
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "Nomina.xlsx");
                }
            }
        }

        public void loadTable(DataTable _dataTable)
        {
            List<Reportes> lista = new List<Reportes>();
            for (int i=0; i < _dataTable.Rows.Count; i++)
            {
                Reportes r = new Reportes();
                r.usuario = _dataTable.Rows[i]["usuario"].ToString();
                r.cargo = _dataTable.Rows[i]["Rol"].ToString();  
                r.salaraio = _dataTable.Rows[i]["salario"].ToString();
                r.fecha = _dataTable.Rows[i]["fecha_ingreso"].ToString();
                lista.Add(r);
            }
            ViewBag.Nomina = lista;
        }

    }

    }