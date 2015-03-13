using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace validate_env_var_paths {

	class Program {

		static void Main(string[] args) {

			var evs = Environment.GetEnvironmentVariables();
			var keys = evs.Keys.Cast<string>().OrderBy(s => s);
			foreach (var key in keys) {
				checkEnvVar(key, (string)evs[key]);
			}
		}


		private static void checkEnvVar(string ev, string val) {

			Console.WriteLine("==== {0}:\t {1}", ev, val);
			if (!val.Contains(":")) { return; }

			if (!val.Contains(";")) {
				testPath(val);
			}else {
				string[] paths = val.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
				foreach (var path in paths) {
					testPath(path);
				}
			}
		}

		private static void testPath(string path) {

			if (File.Exists(path)) {
				Console.WriteLine("     OK [file]: {0}", path);
			} else if (Directory.Exists(path)) {
				Console.WriteLine("     OK [dir]: {0}", path);
			} else {
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("     NOT OK [does not exists]: {0}", path);
				Console.ResetColor();
			}
		}

	}
}
