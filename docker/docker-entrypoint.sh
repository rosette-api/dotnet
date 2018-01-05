#!/bin/bash

cp -r /source/* /workspace/source/
cd /workspace/source
rm -rf `find -type d -name "obj" -o -name "bin"`
mv examples examples-src
mkdir examples
cp /scripts/examples.csproj examples

dotnet clean
dotnet build
dotnet pack --no-build --no-restore
dotnet test --no-build --no-restore ./tests/tests.csproj

for filename in ./examples-src/*.cs; do
    echo "-------------- $(basename ${filename}) -----------------------"
    cp ${filename} examples
    pushd examples > /dev/null
    dotnet build
    if [ -z ${ALT_URL} ]; then
        dotnet run ${API_KEY}
    else
        dotnet run ${API_KEY} ${ALT_URL}
    fi
    rm $(basename "${filename}")
    popd > /dev/null
done