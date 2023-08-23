using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using CoreRepo.Benchmark;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, World!");
var a  = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.Development.json", true, true)
    .Build();

var b = a.GetConnectionString("CoreDB");

var config = DefaultConfig.Instance
    .WithOptions(ConfigOptions.DisableOptimizationsValidator);
var summary = BenchmarkRunner.Run<ControllerBenchmark>(config);