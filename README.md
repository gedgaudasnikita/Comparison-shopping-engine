# Comparison-shopping-engine

## Setup
~~Since NuGet is developed by Microsoft,~~ when cloning the repository for the first time (or you dont have 'x64' and/or 'x86' directories in application root), the easiest way to sort out all the packages for the current runtime is do delete `packages.config` and then run

```
Install-Package Tesseract
```

in the Package Manager Console. 

Reasonale: `nuget restore` does not run PowerShell scripts, and Tesseract, for some reason, uses PowerShell scripts.

## Caveats
If for some reason, when running unit tests or trying to build the project, VS starts throwing up with errors like "failed to copy *.dll" or something similar, just restart VS, most of the time it does the trick.
