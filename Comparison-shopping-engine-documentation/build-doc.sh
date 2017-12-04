#!/bin/sh

#Original: https://gist.github.com/vidavidorra/548ffbcdae99d752da02

#if [ $TRAVIS_BRANCH != 'master' ]; then
#	exit 0
#fi

set -e

mkdir code_docs
cd code_docs

git clone -b gh-pages https://git@$GH_REPO_REF
cd $GH_REPO_NAME

git config --global push.default simple
git config user.name "Travis CI"
git config user.email "travis@travis-ci.org"

rm -rf *

echo "" > .nojekyll
echo ${TRAVIS_BUILD_NUMBER} > .version

../../coveragerunner/OpenCover.Console.exe -target:"../../testrunner/NUnit.ConsoleRunner.3.7.0/tools/nunit3-console.exe" -targetargs:"../../Comparison-shopping-engine-backend.Tests/bin/CI/Comparison-shopping-engine-backend.Tests.dll" -filter:"+[*]* -[Comparison-shopping-engine-backend.Tests*]*" -register
../../coveragereporter/ReportGenerator.exe -reports:results.xml -targetdir:coverage

doxygen $DOXYFILE 2>&1 | tee doxygen.log

if [ -d "html" ] && [ -f "html/index.html" ]; then
    mv html/* .
    rm -rf html
    git add --all
    git commit -m "Deploy code docs to GitHub Pages Travis build: ${TRAVIS_BUILD_NUMBER}" -m "Commit: ${TRAVIS_COMMIT}"
	
    git push --force "https://${GH_REPO_TOKEN}@${GH_REPO_REF}" > /dev/null 2>&1
else
    echo '' >&2
    echo 'Warning: No documentation (html) files have been found!' >&2
    echo 'Warning: Not going to push the documentation to GitHub!' >&2
    exit 1
fi
