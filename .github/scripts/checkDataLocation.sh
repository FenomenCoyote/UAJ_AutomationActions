mistake=0

find . -type f -name *.asset -or -name *.json -or -name *.txt -or -name *.xaml -or -name *.xml -or -name *.csv | while read file
do
	if [[ $file != ./Assets/Data/* ]] 
	then 
		echo "file: $file is not in Data folder"	
		mistake=1
	fi
done

if [ $mistake == 0 ]; then
	echo "All data files are correctly located"
fi

exit $mistake

