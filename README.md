[![Builds](https://travis-ci.org/gedgaudasnikita/Comparison-shopping-engine.svg?branch=master)](https://travis-ci.org/gedgaudasnikita/Comparison-shopping-engine) [![Coverage](https://codecov.io/gh/gedgaudasnikita/Comparison-shopping-engine/branch/master/graph/badge.svg)](https://codecov.io/gh/gedgaudasnikita/Comparison-shopping-engine)
# Comparison-shopping-engine
## Description
A mobile application, letting the user compare the prices of his purchased items with the ones, submitted by other users.
This project is created as a part of the Vilnius University Software Engineering coursework assignment.

## Setup
~~Since NuGet is developed by Microsoft,~~ when cloning the repository for the first time you might get a hell lot of warnings and errors. And that is A-OK 🙂. Run

```
Update-Package –reinstall
```

in the Package Manager Console and don't ask any questions, because i don't know the answers and i don't even want to know them at this point.

---

If you want to boot the entire system (both back-end and front-end) when you run the solution (F5) in VS, right-click on the solution, navigate to `Properties` -> `Common Properties` -> `Startup Project`, select `Multiple startup projects` and set action `Start` on projects `Comparison-shopping-engine-backend` and `Comparison-shopping-engine-frontend-android`. All the rest should be set to `None`.

## Documentation
You can always find the latest code documentation in [GitHub Pages](https://gedgaudasnikita.github.io/Comparison-shopping-engine/).
