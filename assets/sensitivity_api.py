"""
This is the main python extension used to run sensitivity analysis bundled in SWAT+ toolbox software

Created : 23/06/2020
Author  : Celray James
Email   : celray.chawanda@outlook.com
Licence : MIT 2020 (or later)
"""
# imports
import numpy, pickle, sys
from SALib.analyze import sobol, fast, rbd_fast, delta
from SALib.sample import saltelli, fast_sampler, latin

# functions
def read_from(filename):
    '''
    a function to read ascii files
    '''
    g = open(filename, 'r')
    file_text = g.readlines()
    g.close()
    return file_text

def define_a_problem(parameter_names: list, min_max_bounds:list):
    problem = {"num_vars": len(parameter_names),"names": parameter_names,"bounds": min_max_bounds}
    return problem

def sample_parameters(problem:dict, seed:int, method:str):
    if method == "FAST":
        return fast_sampler.sample(problem, seed)
    if method == "sobol":
        return saltelli.sample(problem, seed)
    if (method == "RBD_FAST") or (method == "DMIM"):
        return latin.sample(problem, seed)


print("\n\n")
for i in sys.argv:
    print(i)
print("\n\n")

command = sys.argv[1]
sensitivity_method = sys.argv[2]

# generate sample code
if command == "generate_sample":
        
    # get option
    txtinout = sys.argv[3].replace("__space__", " ")
    seed = int(sys.argv[4])

    parameters = []
    bounds = []

    for line in read_from(f"{txtinout}/par_data.stb")[1:]:
        line = line.strip("\n")
        name, min, max = line.split(",")
        parameters.append(name)
        bounds.append([float(min), float(max)])

    problem = define_a_problem(parameters, bounds)
    sample = sample_parameters(problem, seed=seed, method = sensitivity_method)

    numpy.savetxt(f"{txtinout}/par_sample.stb", sample, delimiter=",", newline="\n")
    with open(f'{txtinout}/sens_def.stb', 'wb') as f:
        pickle.dump(problem, f)
    with open(f'{txtinout}/sample_def.stb', 'wb') as f:
        pickle.dump(sample, f)

# calculate sensitivity code
if command == "analyse_sensitivity":

    # get option
    txtinout = sys.argv[3].replace("__space__", " ")
    report_file = f"{txtinout}/perf_report.stb"
    par_performance = numpy.loadtxt(report_file)

    # load problem definition
    with open(f"{txtinout}/sens_def.stb", "rb") as f:
        sens_def = pickle.load(f)

    # Perform analysis
    if sensitivity_method == "RBD_FAST":
        with open(f"{txtinout}/sample_def.stb", "rb") as f:
            sample = pickle.load(f)
        Si = rbd_fast.analyze(sens_def, sample, par_performance, print_to_console=True)
    if sensitivity_method == "DMIM":
        with open(f"{txtinout}/sample_def.stb", "rb") as f:
            sample = pickle.load(f)
        Si = delta.analyze(sens_def, sample, par_performance, print_to_console=True)
    if sensitivity_method == "FAST":
        Si = fast.analyze(sens_def, par_performance, print_to_console=True)
    if sensitivity_method == "sobol":
        Si = sobol.analyze(sens_def, par_performance, print_to_console=False)


    # Save the first-order sensitivity indices
    numpy.savetxt(f"{txtinout}/s1_sensitivity.stb", Si['S1'], delimiter=",", newline="\n")
    # numpy.savetxt(f"{txtinout}/s2_sensitivity.stb", Si['S2'], delimiter=",", newline="\n")
