$ cat git-show-big 
#!/bin/bash

# Ref http://naleid.com/blog/2012/01/17/finding-and-purging-big-files-from-git-history

SHAS=$(git rev-list --objects --all | sort -k 2)
BIGOBJS=$(git gc && git verify-pack -v .git/objects/pack/pack-*.idx | egrep "^\w+ blob\W+[0-9]+ [0-9]+ [0-9]+$" | sort -k 3 -n -r)
join <(echo "$BIGOBJS" | sort ) <(echo "$SHAS" | sort ) | sort -k 3 -n -r | cut -f 1,3,6- -d\ 