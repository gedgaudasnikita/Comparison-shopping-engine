#!/bin/sh

#Original: https://gist.github.com/vidavidorra/548ffbcdae99d752da02

#if [ $APPVEYOR_REPO_BRANCH != 'master' ]; then
#	exit 0
#fi

set -e

mkdir code_docs
cd code_docs

git clone -b gh-pages https://git@$GH_REPO_REF
cd $GH_REPO_NAME

git config --global push.default simple
git config user.name "Appveyor CI"
git config user.email "appveyor@appveyor.org"

rm -rf *

echo "" > .nojekyll
echo ${APPVEYOR_BUILD_NUMBER} > .version

doxygen $DOXYFILE 2>&1 | tee doxygen.log

if [ -d "html" ] && [ -f "html/index.html" ]; then
    mv html/* .
    rm -rf html
    git add --all
    git commit -m "Deploy code docs to GitHub Pages Appveyor build: ${APPVEYOR_BUILD_NUMBER}" -m "Commit: ${APPVEYOR_REPO_COMMIT}"
	
    git push --force "https://${GH_REPO_TOKEN}@${GH_REPO_REF}" > /dev/null 2>&1
else
    echo '' >&2
    echo 'Warning: No documentation (html) files have been found!' >&2
    echo 'Warning: Not going to push the documentation to GitHub!' >&2
    exit 1
fi
