﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace ConsumerClient {

    public static class MefHelper {
        public static AggregateCatalog SafeDirectoryCatalog(string directory) {
            var files = Directory.EnumerateFiles(directory, "*.dll", SearchOption.AllDirectories);

            var catalog = new AggregateCatalog();

            foreach (var file in files) {
                try {
                    var asmCat = new AssemblyCatalog(file);
                    if (asmCat.Parts.ToList().Count > 0)
                        catalog.Catalogs.Add(asmCat);
                } catch (ReflectionTypeLoadException) {
                
                } catch (BadImageFormatException) {
                
                } catch (Exception) {
                
                }
            }

            return catalog;
        }

        public static void ComposeParts(object obj) {
            //AggregateCatalog catalog = new AggregateCatalog(new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory));
            CompositionContainer container = new CompositionContainer(MefHelper.SafeDirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory));
            try {
                container.ComposeParts(obj);
            } catch {

            }
        }

    }
}