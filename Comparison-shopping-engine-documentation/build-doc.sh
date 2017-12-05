#!/bin/sh

#Original: https://gist.github.com/vidavidorra/548ffbcdae99d752da02

#if [ $TRAVIS_BRANCH != 'master' ]; then
#	exit 0
#fi

export LD_LIBRARY_PATH=/usr/local/lib
mono --debug --profile=monocov:outfile=monocovCoverage.cov,+Comparison-shopping-engine-backend ../testrunner/NUnit.ConsoleRunner.3.7.0/tools/nunit3-console.exe --process=Single ../Comparison-shopping-engine-backend.Tests/bin/Release/Comparison-shopping-engine-backend.Tests.dll
monocov --export-xml=monocovCoverage monocovCoverage.cov
cat monocovCoverage.cov
ls monocovCoverage
REPO_COMMIT_AUTHOR=$(git show -s --pretty=format:"%cn")
REPO_COMMIT_AUTHOR_EMAIL=$(git show -s --pretty=format:"%ce")
REPO_COMMIT_MESSAGE=$(git show -s --pretty=format:"%s")
echo $TRAVIS_COMMIT
echo $TRAVIS_BRANCH
echo $REPO_COMMIT_AUTHOR
echo $REPO_COMMIT_AUTHOR_EMAIL
echo $REPO_COMMIT_MESSAGE
echo $TRAVIS_JOB_ID
mono ../packages/coveralls.net.0.6.0/tools/csmacnz.Coveralls.exe --monocov -i ./monocovCoverage --repoToken $COVERALLS_REPO_TOKEN --commitId $TRAVIS_COMMIT --commitBranch $TRAVIS_BRANCH --commitAuthor "$REPO_COMMIT_AUTHOR" --commitEmail "$REPO_COMMIT_AUTHOR_EMAIL" --commitMessage "$REPO_COMMIT_MESSAGE" --jobId $TRAVIS_JOB_ID  --serviceName travis-ci  --useRelativePaths

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
