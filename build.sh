#!/bin/bash

dotnet restore

# Put your test project directory here (and params, if any)
dotnet test Olekstra.LikePharma.Client.Tests/Olekstra.LikePharma.Client.Tests.csproj
exitcode1=$?

dotnet test Olekstra.LikePharma.Server.Tests/Olekstra.LikePharma.Server.Tests.csproj
exitcode2=$?

if [ -z "$$BUILDSTATUS_WEBHOOK_URL" ]; then
    echo "Add BUILDSTATUS_WEBHOOK_URL variable to enable reporting"
    exit 1
fi

name=$BITBUCKET_REPO_SLUG
url=https://bitbucket.org/$BITBUCKET_REPO_OWNER/$BITBUCKET_REPO_SLUG/addon/pipelines/home#!/
commit_url=https://bitbucket.org/$BITBUCKET_REPO_OWNER/$BITBUCKET_REPO_SLUG/commits/$BITBUCKET_COMMIT
commit_hash=${BITBUCKET_COMMIT:0:7}
branch=`git rev-parse --abbrev-ref HEAD`
commit_author=`git log -1 --format=%an`
commit_timestamp=`git log -1 --format=%at`
commit_message=`git log -1 --format=%s`
#git log -1 --format=%B > commit_message
status="Unknown :("
color="warning"
globalexitcode=1

if [ $exitcode1 -eq 0 ] && [ $exitcode2 -eq 0 ]; then
    color="good"
    status="succeeded"
    globalexitcode=0
else
    color="danger"
    status="FAILED"
fi

echo
echo
echo "Reporting status..."
echo "  name:             $name"
echo "  branch:           $branch"
echo "  commit_url:       $commit_url"
echo "  commit_hash:      $commit_hash"
echo "  commit_author:    $commit_author"
echo "  commit_timestamp: $commit_timestamp"
echo "  status:           $status"
echo "  commit_message:   $commit_message"
#cat commit_message

curl $BUILDSTATUS_WEBHOOK_URL \
    --data-urlencode repo_name="$name" \
    --data-urlencode branch="$branch" \
    --data-urlencode commit_url="$commit_url" \
    --data-urlencode commit_hash="$commit_hash" \
    --data-urlencode commit_author="$commit_author" \
    --data-urlencode commit_timestamp="$commit_timestamp" \
    --data-urlencode commit_message="$commit_message" \
    --data-urlencode status="$status"

exit $globalexitcode