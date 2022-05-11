maxLen=32
myvar=0
cd Assets/
while read file
do
	f="$(basename "$file" )"
	len=`expr length "$f"`
	if [[ $len -gt $maxLen ]]
	then
		echo $f
		echo $len
		echo "ChFilenamesLength: name of file $file has more than $maxLen characters"
		myvar=1
	fi
done <<< "$(find . -type f ! -name '*.meta')"
if [ $myvar == 0 ]
then
	echo "ChFilenamesLength: success"
fi
exit $myvar

