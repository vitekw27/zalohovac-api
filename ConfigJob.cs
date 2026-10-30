using Org.BouncyCastle.Cms;
using System;
using System.Collections.Generic;
using ZalohovacServer.Models;

namespace ZalohovacServer
{
    public class ConfigJob
    {
        private MyContext context;

        public ConfigJob(MyContext context)
        {
            this.context = context;
        }

        public List<Config> GetConfigs2()
        {
            List<Sources> sources = context.Sources.ToList(); 
            List<Targets> targets = context.Targets.ToList();
            
            List<Config> configs = new List<Config>();
            List<ConfigDataSources> ConfigDataSources = context.ConfigDataSources_1.ToList();
            List<ConfigDataTarget> ConfigDataTarget = context.ConfigDataTargets_1.ToList();

            foreach (ConfigData data in context.ConfigData)
            {
                Config config = new Config();

                foreach (ConfigDataTarget dataTarget in ConfigDataTarget)
                {
                    if (data.id == dataTarget.idConfigData)
                    {
                        foreach (Targets target in targets)
                        {
                            if (target.id == dataTarget.idTargets)
                            {
                                if (config.Targets == null)
                                {
                                    config.Targets = new List<string>(); 
                                }
                                config.Targets.Add(target.target);
                            }
                        }
                    }
                }
                foreach (ConfigDataSources dataSource in ConfigDataSources)
                {
                    if (data.id == dataSource.idConfigData)
                    {
                        foreach (Sources source in sources)
                        {
                            if (source.id == dataSource.idSources)
                            {
                                if (config.Sources == null)
                                {
                                    config.Sources = new List<string>(); 
                                }
                                config.Sources.Add(source.source);
                            }
                        }
                    }
                }



                config.RetSize = data.Size;
                config.RetCount = data.Count; 
                config.Name = data.name;
                config.id = data.id;
                config.Method = data.Method;
                config.Timing = data.Timing;

                configs.Add(config);
            }

            return configs;
        }

        public List<Config> GetConfigs()
        {
            var sources = context.Sources.ToList();
            var targets = context.Targets.ToList();
            var configDataList = context.ConfigData.ToList();
            var configDataSources = context.ConfigDataSources_1.ToList();
            var configDataTargets = context.ConfigDataTargets_1.ToList();
            var configs = new List<Config>();
            foreach (var data in configDataList) 
            {
                var config = new Config
                {
                    id = data.id,
                    RetSize = data.Size,
                    RetCount = data.Count,
                    Name = data.name,
                    Method = data.Method,
                    Timing = data.Timing,
                }; 
                var targetIds = configDataTargets.Where(d=>d.idConfigData == data.id).SelectMany(d=>targets.Where(t=>t.id ==d.idTargets)).Select(t=>t.target).ToList();
                if (targetIds.Any())
                {
                    config.Targets = targetIds;
                }
                var sourceIds = configDataSources.Where(d=>d.idConfigData==data.id).SelectMany(d=>sources.Where(s=>s.id == d.idSources)).Select(s=>s.source).ToList(); 
                if(sourceIds.Any())
                {
                    config.Sources = sourceIds;
                }
                configs.Add(config);
            }
            return configs;
        }

        public void AddConifgs(List<Config> configs)
        {
            

            foreach(Config config in configs)
            {
               
                context.SaveChanges();
                
                ConfigData configData = new ConfigData() { Count = config.RetCount,Size = config.RetSize, Method = config.Method, name = config.Name, Timing  =config.Timing};
                context.ConfigData.Add(configData);
                context.SaveChanges();
                foreach (string targCon in config.Targets)
                {
                    Targets target = new Targets() { target = targCon};
                    context.Targets.Add(target);
                    context.SaveChanges();
                    ConfigDataTarget cdt = new ConfigDataTarget() { idConfigData = GetLastIdConfigData(), idTargets = GetLastIdTargets() };
                    context.ConfigDataTargets_1.Add(cdt); 
                    context.SaveChanges();

                }
                foreach(string srcCon in config.Sources)
                {
                    Sources source = new Sources() { source = srcCon };
                    context.Sources.Add(source);
                    context.SaveChanges();
                    ConfigDataSources cds = new ConfigDataSources() { idConfigData = GetLastIdConfigData(), idSources = GetLastIdSources() };
                    context.ConfigDataSources_1.Add(cds);
                    context.SaveChanges();
                }

            }
        }
        public Config GetConfig(int id)
        {
            foreach (var config in GetConfigs())
            {
                if (config.id == id)
                {
                    return config;
                }
            }
            return null; 
        }

        public void DeleteConfig(int id)
        {
           
            foreach (ConfigData cd in context.ConfigData)
            {
                if(cd.id == id)
                {
                    context.ConfigData.Remove(cd);
                }
               
            }
            context.SaveChanges();
            //foreach (ConfigDataSources cds in context.ConfigDataSources_1)
            //{
            //    if(id == cds.idConfigData)
            //    {
            //        context.ConfigDataSources_1.Remove(cds);
            //    }
            //}
            //foreach (ConfigDataTarget cds in context.ConfigDataTargets_1)
            //{
            //    if (id == cds.idConfigData)
            //    {
            //        context.ConfigDataTargets_1.Remove(cds);
            //    }
            //}
            


        }
        public void UpdateConfig(int id,Config newCon)
        {
            // Remove existing targets and sources related to this config
            context.ConfigDataTargets_1.RemoveRange(context.ConfigDataTargets_1.Where(cdt => cdt.idConfigData == id));
            context.ConfigDataSources_1.RemoveRange(context.ConfigDataSources_1.Where(ds => ds.idConfigData == id));

            // Save changes to remove existing targets and sources
            context.SaveChanges();
            List<Targets> targets = new List<Targets>();
            // Add new targets
            foreach (string target in newCon.Targets)
            {
                Targets targModel = new Targets() { target = target };
                context.Targets.Add(targModel);
                targets.Add(targModel);
                context.SaveChanges();




            }
            
            for (int i = 0; i < targets.Count; i++)
            {
                ;
                ConfigDataTarget cdt = new ConfigDataTarget() { idConfigData = id, idTargets = GetLastIdTargets() - i };
                context.ConfigDataTargets_1.Add(cdt);


            }
            Console.WriteLine(id);
            context.SaveChanges();

            List<Sources> sources = new List<Sources>();

            // Add new sources
            foreach (string source in newCon.Sources)
            {
                Sources srcModel = new Sources() { source = source };
                context.Sources.Add(srcModel);
                sources.Add(srcModel);
                context.SaveChanges();



            }
            Thread.Sleep(500);

            for (int i = 0; i < sources.Count; i++)
            {
                ConfigDataSources cdt = new ConfigDataSources() { idConfigData = id, idSources = GetLastIdSources() - i };
                context.ConfigDataSources_1.Add(cdt);


            }
            context.SaveChanges();


            // Save changes to add new targets and sources


            // Update the config data
            ConfigData test = new ConfigData()
            {
                id = id,
                Method = newCon.Method,
                Size = newCon.RetSize,
                Count = newCon.RetCount,
                Timing = newCon.Timing,
                name = newCon.Name
            };
            context.ConfigData.Update(test);
            context.SaveChanges();

        }

        public int GetLastIdConfigData()
        {
            int highest = 0;
            foreach(ConfigData data in context.ConfigData)
            {
                if(data.id > highest)
                {
                    highest = data.id;
                }
            }
            return highest; 
        }
        public int GetLastIdSources()
        {
            int highest = 0;
            foreach (Sources data in context.Sources)
            {
                if (data.id > highest)
                {
                    highest = data.id;
                }
            }
            return highest;
        }
        public int GetLastIdTargets()
        {
            int highest = 0;
            foreach (Targets data in context.Targets)
            {
                if (data.id > highest)
                {
                    highest = data.id;
                }
            }
            return highest;
        }

        //public List<Config> GetConfigsByToken(string token)
        //{
        //    List<Config> configs = this.GetConfigs(); 
        //}
       
    }
}