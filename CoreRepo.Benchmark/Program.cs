using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using CoreRepo.Benchmark;
using Microsoft.Extensions.Configuration;

var a  = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", true, true)
    .Build();

var b = a.GetConnectionString("CoreDB");

var config = DefaultConfig.Instance
    .WithOptions(ConfigOptions.DisableOptimizationsValidator);
var summary = BenchmarkRunner.Run<DataBenchmark>(config);
