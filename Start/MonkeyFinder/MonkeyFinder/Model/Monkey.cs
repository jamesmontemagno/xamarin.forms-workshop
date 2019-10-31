// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using MonkeyFinder.Model;
//
//    var monkeys = Monkey.FromJson(jsonString);

using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MonkeyFinder.Model
{
    public partial class Monkey
    {
    }
}
