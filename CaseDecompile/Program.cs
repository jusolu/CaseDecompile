using System;
using System.IO;
using NUnit.Framework;
using AgGateway.ADAPT.PluginManager;
using AgGateway.ADAPT.ApplicationDataModel.ADM;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using AgGateway.ADAPT.ApplicationDataModel.LoggedData;
using NetTopologySuite.Geometries;
using System.Linq;


namespace CaseDecompile
{
    class Program
    {
        public static string pluginDirectory = @"C:\CaseIH_NH\Voyager2ADAPTPlugin\Plugin";
        //public static string sampleFile = @"C:\CaseIH_NH\Voyager2ADAPTPlugin\SampleFiles\Voyager2_Sample.zip";
        public static string pluginName = "Voyager2Plugin";

        bool exists = Directory.Exists(pluginDirectory);

        PluginFactory factory;
        IPlugin plugin;
        private OperationDataProcessor ope_data_process = new OperationDataProcessor();
        public Program()
        {
            factory = new PluginFactory(pluginDirectory);
            plugin = factory.GetPlugin(pluginName);
            IList<ApplicationDataModel> admModels = plugin.Import("C:\\CaseIH_NH\\Voyager2ADAPTPlugin\\SampleFiles\\2025-02-18-18-12-07");
            ApplicationDataModel adm = admModels[0];
            var a = adm.Catalog;
            var b = adm.Documents;

            //var teste = ope_data_process.ProcessOperationData(adm.Documents.);


            foreach (var doc in adm.Documents.LoggedData)
            {
                foreach (var opData in doc.OperationData)
                {
                    //var teste = ope_data_process.ProcessOperationData(adm.Documents.);

                    if (opData.GetSpatialRecords != null)
                    {
                        IEnumerable<SpatialRecord> spatialRecords = opData.GetSpatialRecords();

                        var teste = ope_data_process.ProcessOperationData(opData, spatialRecords.ToList());

                        List<Geometry> geometries = new List<Geometry>();
                        List<Dictionary<string, object>> attributes = new List<Dictionary<string, object>>();

                        foreach (var record in spatialRecords)
                        {
                            foreach (var contextItem in record.Geometry.ContextItems)
                            {
                                Console.WriteLine(contextItem.Code);
                                Console.WriteLine(contextItem.Value);
                                Console.WriteLine(contextItem.ValueUOM);
                                Console.WriteLine(contextItem.TimeScopes.Count);
                                Console.WriteLine(contextItem.NestedItems.Count);
                            }
                           
                        }

                    }
                }
            }
            //adm.Documents.Summaries

            //foreach (var item_a1 in adm.Documents.IrrRecords)
            //{
            //    Console.WriteLine($"item_a1: {item_a1.Notes}");
            //}
            //foreach (var item_a1 in adm.Documents.Summaries) {
            //    Console.WriteLine($"item_a1: {item_a1}");
            //    //item_a1.OperationSummaries[0].Data[0].Values[0].Value
            //    foreach (var item_a2 in item_a1.OperationSummaries)
            //    {
            //        Console.WriteLine($"item_a2: {item_a2.Description}");
            //        Console.WriteLine($"item_a2: {item_a2.CoverageShape}");
            //        Console.WriteLine($"item_a2: {item_a2.ContextItems}");
            //        Console.WriteLine($"item_a2: {item_a2.EquipmentConfigurationIds}");
            //        Console.WriteLine($"item_a2: {item_a2.ProductId}");

            //        foreach (var item3 in item_a2.Data)
            //        {
            //            Console.WriteLine($"item3: {item3.Stamp}");
            //            Console.WriteLine($"item3: {item3.Values}");
            //            foreach (var item4 in item3.Values)
            //            {
            //                Console.WriteLine($"item3: {item4.Value}");
            //                Console.WriteLine($"item3: {item4.MeterId}");
            //            }
            //        }
            //    }

            //}


            //foreach (var item1 in adm.Documents.LoggedData)
            //{
            //    Console.WriteLine("1");
            //    foreach (var item2 in item1.OperationData)
            //    {
            //        Console.WriteLine($"item2: {item2.Id.UniqueIds}");
            //        Console.WriteLine($"item2: {item2.LoadId}");
            //        Console.WriteLine($"item2: {item2.ProductIds}");
            //        Console.WriteLine($"item2: {item2.ContextItems}");
            //        Console.WriteLine($"item2: {item2.Description}");
            //        Console.WriteLine($"item2: {item2.GetDeviceElementUses}");
            //        Console.WriteLine($"item2: {item2.GetSpatialRecords}");
            //        Console.WriteLine($"item2: {item2.OperationType}");
            //        Console.WriteLine($"item2: {item2.VarietyLocatorId}");
            //        Console.WriteLine($"item2: {item2.CoincidentOperationDataIds}");
            //        foreach (var item3 in item2.GetSpatialRecords())
            //        {
            //            Console.WriteLine($"item3: {item3.Geometry.Id}");
            //            Console.WriteLine($"item3: {item3.Geometry.ContextItems}");
            //            Console.WriteLine($"item3: {item3.Geometry.Type}");
            //            Console.WriteLine($"item3: {item3.SignalType}");
            //            Console.WriteLine($"item3: {item3.Timestamp}");
            //            //Console.WriteLine($"item3: {item3.Geometry.ContextItems[0].Value}");
            //            //Console.WriteLine($"item3: {item3.Geometry.ContextItems[0].Code}");
            //        }

            //    }

            //}

            //foreach (var item in adm.Documents.LoggedData)
            //{
            //    Console.WriteLine("1");

            //}
            ////ForEach adm.Documents.Summaries
            //foreach (var item in adm.Documents.WorkRecords)
            //{
            //    foreach (var item2 in item.Notes)
            //    {
            //        Console.WriteLine(item2.SpatialContext);

            //    }

            //    Console.WriteLine("1");

            //}

            

            Console.WriteLine("-------------");


        }

        static void Main(string[] args)
        {
            Program program = new Program();
            Console.WriteLine("BEGIN");
        }
    }
    
}
