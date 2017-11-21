#!/bin/bash
# server=build.palaso.org
# project=WeSay
# build=wesay1.6-win32-continuous
# root_dir=..
# Auto-generated by https://github.com/chrisvire/BuildUpdate.
# Do not edit this file by hand!

cd "$(dirname "$0")"

# *** Functions ***
force=0
clean=0

while getopts fc opt; do
case $opt in
f) force=1 ;;
c) clean=1 ;;
esac
done

shift $((OPTIND - 1))

copy_auto() {
if [ "$clean" == "1" ]
then
echo cleaning $2
rm -f ""$2""
else
where_curl=$(type -P curl)
where_wget=$(type -P wget)
if [ "$where_curl" != "" ]
then
copy_curl $1 $2
elif [ "$where_wget" != "" ]
then
copy_wget $1 $2
else
echo "Missing curl or wget"
exit 1
fi
fi
}

copy_curl() {
echo "curl: $2 <= $1"
if [ -e "$2" ] && [ "$force" != "1" ]
then
curl -# -L -z $2 -o $2 $1
else
curl -# -L -o $2 $1
fi
}

copy_wget() {
echo "wget: $2 <= $1"
f1=$(basename $1)
f2=$(basename $2)
cd $(dirname $2)
wget -q -L -N $1
# wget has no true equivalent of curl's -o option.
# Different versions of wget handle (or not) % escaping differently.
# A URL query is the only reason why $f1 and $f2 should differ.
if [ "$f1" != "$f2" ]; then mv $f2\?* $f2; fi
cd -
}


# *** Results ***
# build: wesay1.6-win32-continuous (bt170)
# project: WeSay
# URL: http://build.palaso.org/viewType.html?buildTypeId=bt170
# VCS: https://github.com/sillsdev/wesay.git [refs/heads/master]
# dependencies:
# [0] build: chorus-win32-chorus-2.5-nostrongname Continuous (ChorusWin32v25nostrongCont)
#     project: Chorus
#     URL: http://build.palaso.org/viewType.html?buildTypeId=ChorusWin32v25nostrongCont
#     clean: false
#     revision: wesay-1.6.5.tcbuildtag
#     paths: {"Chorus.exe"=>"lib/Release", "Chorus.pdb"=>"lib/Release", "ChorusMerge.exe"=>"lib/Release", "ChorusMerge.pdb"=>"lib/Release", "LibChorus.dll"=>"lib/Release", "LibChorus.pdb"=>"lib/Release", "LibChorus.TestUtilities.dll"=>"lib/Release", "LibChorus.TestUtilities.pdb"=>"lib/Release", "Mercurial.zip"=>"lib/Release", "MercurialExtensions/**"=>"MercurialExtensions", "ChorusMergeModule.msm"=>"lib", "debug/Chorus.exe"=>"lib/Debug", "debug/Chorus.pdb"=>"lib/Debug", "debug/ChorusHub.exe"=>"lib/Debug", "debug/ChorusHub.pdb"=>"lib/Debug", "debug/ChorusMerge.exe"=>"lib/Debug", "debug/ChorusMerge.pdb"=>"lib/Debug", "debug/LibChorus.dll"=>"lib/Debug", "debug/LibChorus.pdb"=>"lib/Debug", "debug/LibChorus.TestUtilities.dll"=>"lib/Debug", "debug/LibChorus.TestUtilities.pdb"=>"lib/Debug"}
#     VCS: https://github.com/sillsdev/chorus.git [chorus-2.5]
# [1] build: geckofx29-win32-continuous (bt399)
#     project: GeckoFx
#     URL: http://build.palaso.org/viewType.html?buildTypeId=bt399
#     clean: false
#     revision: latest.lastSuccessful
#     paths: {"Geckofx-Core.dll"=>"lib/Release", "Geckofx-Core.dll.config"=>"lib/Release", "Geckofx-Winforms.dll"=>"lib/Release", "Geckofx-Winforms.pdb"=>"lib/Release"}
#     VCS: https://bitbucket.org/geckofx/geckofx-29.0 [default]
# [2] build: geckofx29-win32-continuous (bt399)
#     project: GeckoFx
#     URL: http://build.palaso.org/viewType.html?buildTypeId=bt399
#     clean: false
#     revision: latest.lastSuccessful
#     paths: {"Geckofx-Core.dll"=>"lib/Debug", "Geckofx-Core.dll.config"=>"lib/Debug", "Geckofx-Winforms.dll"=>"lib/Debug", "Geckofx-Winforms.pdb"=>"lib/Debug"}
#     VCS: https://bitbucket.org/geckofx/geckofx-29.0 [default]
# [3] build: XulRunner29-win32 (bt400)
#     project: GeckoFx
#     URL: http://build.palaso.org/viewType.html?buildTypeId=bt400
#     clean: false
#     revision: latest.lastSuccessful
#     paths: {"xulrunner-29.0.1.en-US.win32.zip!**"=>""}
# [4] build: L10NSharp Version2.0 continuous (bt196)
#     project: L10NSharp
#     URL: http://build.palaso.org/viewType.html?buildTypeId=bt196
#     clean: false
#     revision: l10nsharp-2.0.tcbuildtag
#     paths: {"L10NSharp.dll"=>"lib/Debug", "L10NSharp.pdb"=>"lib/Debug"}
#     VCS: https://github.com/sillsdev/l10nsharp [Version2.0]
# [5] build: L10NSharp Version2.0 continuous (bt196)
#     project: L10NSharp
#     URL: http://build.palaso.org/viewType.html?buildTypeId=bt196
#     clean: false
#     revision: l10nsharp-2.0.tcbuildtag
#     paths: {"L10NSharp.dll"=>"lib/Release", "L10NSharp.pdb"=>"lib/Release"}
#     VCS: https://github.com/sillsdev/l10nsharp [Version2.0]
# [6] build: wesay-doc-default (bt184)
#     project: WeSay1.4
#     URL: http://build.palaso.org/viewType.html?buildTypeId=bt184
#     clean: false
#     revision: latest.lastSuccessful
#     paths: {"WeSay_Helps.chm"=>"External", "wesay.helpmap"=>"External"}
#     VCS: http://hg.palaso.org/wesay-doc []
# [7] build: wesay-localize-dev Update Pot and Po (bt52)
#     project: WeSay1.4
#     URL: http://build.palaso.org/viewType.html?buildTypeId=bt52
#     clean: false
#     revision: latest.lastFinished
#     paths: {"*.po"=>"common"}
#     VCS: http://hg.palaso.org/wesay-tx []
# [8] build: palaso-win32-libpalaso-2.6-nostrongname Continuous (PalasoWin32v26nostrongCont)
#     project: libpalaso
#     URL: http://build.palaso.org/viewType.html?buildTypeId=PalasoWin32v26nostrongCont
#     clean: false
#     revision: wesay-1.6.5.tcbuildtag
#     paths: {"Palaso.dll"=>"lib/Release", "Palaso.pdb"=>"lib/Release", "Palaso.DictionaryServices.dll"=>"lib/Release", "Palaso.DictionaryServices.pdb"=>"lib/Release", "Palaso.Lift.dll"=>"lib/Release", "Palaso.Lift.pdb"=>"lib/Release", "Palaso.Media.dll"=>"lib/Release", "Palaso.Media.pdb"=>"lib/Release", "Palaso.Tests.dll"=>"lib/Release", "Palaso.Tests.pdb"=>"lib/Release", "Palaso.TestUtilities.dll"=>"lib/Release", "Palaso.TestUtilities.pdb"=>"lib/Release", "PalasoUIWindowsForms.dll"=>"lib/Release", "PalasoUIWindowsForms.pdb"=>"lib/Release", "PalasoUIWindowsForms.GeckoBrowserAdapter.dll"=>"lib/Release", "PalasoUIWindowsForms.GeckoBrowserAdapter.pdb"=>"lib/Release", "Interop.WIA.dll"=>"lib/Release", "taglib-sharp.dll"=>"lib/Release", "debug/**"=>"lib/Debug"}
#     VCS: https://github.com/sillsdev/libpalaso.git []

# make sure output directories exist
mkdir -p ../
mkdir -p ../Downloads
mkdir -p ../External
mkdir -p ../MercurialExtensions
mkdir -p ../MercurialExtensions/fixutf8
mkdir -p ../common
mkdir -p ../lib
mkdir -p ../lib/Debug
mkdir -p ../lib/Release

# download artifact dependencies
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/Chorus.exe ../lib/Release/Chorus.exe
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/Chorus.pdb ../lib/Release/Chorus.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/ChorusMerge.exe ../lib/Release/ChorusMerge.exe
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/ChorusMerge.pdb ../lib/Release/ChorusMerge.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/LibChorus.dll ../lib/Release/LibChorus.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/LibChorus.pdb ../lib/Release/LibChorus.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/LibChorus.TestUtilities.dll ../lib/Release/LibChorus.TestUtilities.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/LibChorus.TestUtilities.pdb ../lib/Release/LibChorus.TestUtilities.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/Mercurial.zip ../lib/Release/Mercurial.zip
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/MercurialExtensions/.guidsForInstaller.xml ../MercurialExtensions/.guidsForInstaller.xml
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/MercurialExtensions/Dummy.txt ../MercurialExtensions/Dummy.txt
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/MercurialExtensions/fixutf8/.gitignore ../MercurialExtensions/fixutf8/.gitignore
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/MercurialExtensions/fixutf8/.guidsForInstaller.xml ../MercurialExtensions/fixutf8/.guidsForInstaller.xml
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/MercurialExtensions/fixutf8/.hg_archival.txt ../MercurialExtensions/fixutf8/.hg_archival.txt
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/MercurialExtensions/fixutf8/.hgignore ../MercurialExtensions/fixutf8/.hgignore
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/MercurialExtensions/fixutf8/README. ../MercurialExtensions/fixutf8/README.
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/MercurialExtensions/fixutf8/buildcpmap.py ../MercurialExtensions/fixutf8/buildcpmap.py
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/MercurialExtensions/fixutf8/cpmap.pyc ../MercurialExtensions/fixutf8/cpmap.pyc
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/MercurialExtensions/fixutf8/fixutf8.py ../MercurialExtensions/fixutf8/fixutf8.py
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/MercurialExtensions/fixutf8/fixutf8.pyc ../MercurialExtensions/fixutf8/fixutf8.pyc
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/MercurialExtensions/fixutf8/fixutf8.pyo ../MercurialExtensions/fixutf8/fixutf8.pyo
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/MercurialExtensions/fixutf8/osutil.py ../MercurialExtensions/fixutf8/osutil.py
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/MercurialExtensions/fixutf8/osutil.pyc ../MercurialExtensions/fixutf8/osutil.pyc
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/MercurialExtensions/fixutf8/osutil.pyo ../MercurialExtensions/fixutf8/osutil.pyo
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/MercurialExtensions/fixutf8/win32helper.py ../MercurialExtensions/fixutf8/win32helper.py
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/MercurialExtensions/fixutf8/win32helper.pyc ../MercurialExtensions/fixutf8/win32helper.pyc
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/MercurialExtensions/fixutf8/win32helper.pyo ../MercurialExtensions/fixutf8/win32helper.pyo
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/ChorusMergeModule.msm ../lib/ChorusMergeModule.msm
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/debug/Chorus.exe ../lib/Debug/Chorus.exe
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/debug/Chorus.pdb ../lib/Debug/Chorus.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/debug/ChorusHub.exe ../lib/Debug/ChorusHub.exe
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/debug/ChorusHub.pdb ../lib/Debug/ChorusHub.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/debug/ChorusMerge.exe ../lib/Debug/ChorusMerge.exe
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/debug/ChorusMerge.pdb ../lib/Debug/ChorusMerge.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/debug/LibChorus.dll ../lib/Debug/LibChorus.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/debug/LibChorus.pdb ../lib/Debug/LibChorus.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/debug/LibChorus.TestUtilities.dll ../lib/Debug/LibChorus.TestUtilities.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/ChorusWin32v25nostrongCont/wesay-1.6.5.tcbuildtag/debug/LibChorus.TestUtilities.pdb ../lib/Debug/LibChorus.TestUtilities.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/bt399/latest.lastSuccessful/Geckofx-Core.dll ../lib/Release/Geckofx-Core.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/bt399/latest.lastSuccessful/Geckofx-Core.dll.config ../lib/Release/Geckofx-Core.dll.config
copy_auto http://build.palaso.org/guestAuth/repository/download/bt399/latest.lastSuccessful/Geckofx-Winforms.dll ../lib/Release/Geckofx-Winforms.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/bt399/latest.lastSuccessful/Geckofx-Winforms.pdb ../lib/Release/Geckofx-Winforms.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/bt399/latest.lastSuccessful/Geckofx-Core.dll ../lib/Debug/Geckofx-Core.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/bt399/latest.lastSuccessful/Geckofx-Core.dll.config ../lib/Debug/Geckofx-Core.dll.config
copy_auto http://build.palaso.org/guestAuth/repository/download/bt399/latest.lastSuccessful/Geckofx-Winforms.dll ../lib/Debug/Geckofx-Winforms.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/bt399/latest.lastSuccessful/Geckofx-Winforms.pdb ../lib/Debug/Geckofx-Winforms.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/bt400/latest.lastSuccessful/xulrunner-29.0.1.en-US.win32.zip ../Downloads/xulrunner-29.0.1.en-US.win32.zip
copy_auto http://build.palaso.org/guestAuth/repository/download/bt196/l10nsharp-2.0.tcbuildtag/L10NSharp.dll ../lib/Debug/L10NSharp.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/bt196/l10nsharp-2.0.tcbuildtag/L10NSharp.pdb ../lib/Debug/L10NSharp.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/bt196/l10nsharp-2.0.tcbuildtag/L10NSharp.dll ../lib/Release/L10NSharp.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/bt196/l10nsharp-2.0.tcbuildtag/L10NSharp.pdb ../lib/Release/L10NSharp.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/bt184/latest.lastSuccessful/WeSay_Helps.chm ../External/WeSay_Helps.chm
copy_auto http://build.palaso.org/guestAuth/repository/download/bt184/latest.lastSuccessful/wesay.helpmap ../External/wesay.helpmap
copy_auto http://build.palaso.org/guestAuth/repository/download/bt52/latest.lastFinished/wesay.az.po ../common/wesay.az.po
copy_auto http://build.palaso.org/guestAuth/repository/download/bt52/latest.lastFinished/wesay.da.po ../common/wesay.da.po
copy_auto http://build.palaso.org/guestAuth/repository/download/bt52/latest.lastFinished/wesay.es.po ../common/wesay.es.po
copy_auto http://build.palaso.org/guestAuth/repository/download/bt52/latest.lastFinished/wesay.fr.po ../common/wesay.fr.po
copy_auto http://build.palaso.org/guestAuth/repository/download/bt52/latest.lastFinished/wesay.he.po ../common/wesay.he.po
copy_auto http://build.palaso.org/guestAuth/repository/download/bt52/latest.lastFinished/wesay.hi.po ../common/wesay.hi.po
copy_auto http://build.palaso.org/guestAuth/repository/download/bt52/latest.lastFinished/wesay.id.po ../common/wesay.id.po
copy_auto http://build.palaso.org/guestAuth/repository/download/bt52/latest.lastFinished/wesay.is.po ../common/wesay.is.po
copy_auto http://build.palaso.org/guestAuth/repository/download/bt52/latest.lastFinished/wesay.km.po ../common/wesay.km.po
copy_auto http://build.palaso.org/guestAuth/repository/download/bt52/latest.lastFinished/wesay.lo.po ../common/wesay.lo.po
copy_auto http://build.palaso.org/guestAuth/repository/download/bt52/latest.lastFinished/wesay.my.po ../common/wesay.my.po
copy_auto http://build.palaso.org/guestAuth/repository/download/bt52/latest.lastFinished/wesay.nl.po ../common/wesay.nl.po
copy_auto http://build.palaso.org/guestAuth/repository/download/bt52/latest.lastFinished/wesay.oc.po ../common/wesay.oc.po
copy_auto http://build.palaso.org/guestAuth/repository/download/bt52/latest.lastFinished/wesay.pl.po ../common/wesay.pl.po
copy_auto http://build.palaso.org/guestAuth/repository/download/bt52/latest.lastFinished/wesay.pt.po ../common/wesay.pt.po
copy_auto http://build.palaso.org/guestAuth/repository/download/bt52/latest.lastFinished/wesay.ru.po ../common/wesay.ru.po
copy_auto http://build.palaso.org/guestAuth/repository/download/bt52/latest.lastFinished/wesay.rw.po ../common/wesay.rw.po
copy_auto http://build.palaso.org/guestAuth/repository/download/bt52/latest.lastFinished/wesay.sv.po ../common/wesay.sv.po
copy_auto http://build.palaso.org/guestAuth/repository/download/bt52/latest.lastFinished/wesay.th.po ../common/wesay.th.po
copy_auto http://build.palaso.org/guestAuth/repository/download/bt52/latest.lastFinished/wesay.tpi.po ../common/wesay.tpi.po
copy_auto http://build.palaso.org/guestAuth/repository/download/bt52/latest.lastFinished/wesay.vi.po ../common/wesay.vi.po
copy_auto http://build.palaso.org/guestAuth/repository/download/bt52/latest.lastFinished/wesay.zh_CN.po ../common/wesay.zh_CN.po
copy_auto http://build.palaso.org/guestAuth/repository/download/bt52/latest.lastFinished/wesay.zh_TW.po ../common/wesay.zh_TW.po
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/Palaso.dll ../lib/Release/Palaso.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/Palaso.pdb ../lib/Release/Palaso.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/Palaso.DictionaryServices.dll ../lib/Release/Palaso.DictionaryServices.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/Palaso.DictionaryServices.pdb ../lib/Release/Palaso.DictionaryServices.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/Palaso.Lift.dll ../lib/Release/Palaso.Lift.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/Palaso.Lift.pdb ../lib/Release/Palaso.Lift.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/Palaso.Media.dll ../lib/Release/Palaso.Media.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/Palaso.Media.pdb ../lib/Release/Palaso.Media.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/Palaso.Tests.dll ../lib/Release/Palaso.Tests.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/Palaso.Tests.pdb ../lib/Release/Palaso.Tests.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/Palaso.TestUtilities.dll ../lib/Release/Palaso.TestUtilities.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/Palaso.TestUtilities.pdb ../lib/Release/Palaso.TestUtilities.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/PalasoUIWindowsForms.dll ../lib/Release/PalasoUIWindowsForms.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/PalasoUIWindowsForms.pdb ../lib/Release/PalasoUIWindowsForms.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/PalasoUIWindowsForms.GeckoBrowserAdapter.dll ../lib/Release/PalasoUIWindowsForms.GeckoBrowserAdapter.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/PalasoUIWindowsForms.GeckoBrowserAdapter.pdb ../lib/Release/PalasoUIWindowsForms.GeckoBrowserAdapter.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/Interop.WIA.dll ../lib/Release/Interop.WIA.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/taglib-sharp.dll ../lib/Release/taglib-sharp.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/debug/Commons.Xml.Relaxng.dll ../lib/Debug/Commons.Xml.Relaxng.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/debug/Interop.WIA.dll ../lib/Debug/Interop.WIA.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/debug/L10NSharp.dll ../lib/Debug/L10NSharp.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/debug/L10NSharp.pdb ../lib/Debug/L10NSharp.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/debug/Palaso.BuildTasks.dll ../lib/Debug/Palaso.BuildTasks.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/debug/Palaso.BuildTasks.pdb ../lib/Debug/Palaso.BuildTasks.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/debug/Palaso.DictionaryServices.dll ../lib/Debug/Palaso.DictionaryServices.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/debug/Palaso.DictionaryServices.pdb ../lib/Debug/Palaso.DictionaryServices.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/debug/Palaso.Lift.dll ../lib/Debug/Palaso.Lift.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/debug/Palaso.Lift.pdb ../lib/Debug/Palaso.Lift.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/debug/Palaso.Media.dll ../lib/Debug/Palaso.Media.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/debug/Palaso.Media.pdb ../lib/Debug/Palaso.Media.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/debug/Palaso.TestUtilities.dll ../lib/Debug/Palaso.TestUtilities.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/debug/Palaso.TestUtilities.pdb ../lib/Debug/Palaso.TestUtilities.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/debug/Palaso.Tests.dll ../lib/Debug/Palaso.Tests.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/debug/Palaso.Tests.pdb ../lib/Debug/Palaso.Tests.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/debug/Palaso.dll ../lib/Debug/Palaso.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/debug/Palaso.pdb ../lib/Debug/Palaso.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/debug/PalasoUIWindowsForms.GeckoBrowserAdapter.dll ../lib/Debug/PalasoUIWindowsForms.GeckoBrowserAdapter.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/debug/PalasoUIWindowsForms.GeckoBrowserAdapter.pdb ../lib/Debug/PalasoUIWindowsForms.GeckoBrowserAdapter.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/debug/PalasoUIWindowsForms.dll ../lib/Debug/PalasoUIWindowsForms.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/debug/PalasoUIWindowsForms.pdb ../lib/Debug/PalasoUIWindowsForms.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/PalasoWin32v26nostrongCont/wesay-1.6.5.tcbuildtag/debug/taglib-sharp.dll ../lib/Debug/taglib-sharp.dll
# extract downloaded zip files
unzip -uqo ../Downloads/xulrunner-29.0.1.en-US.win32.zip -d ../
# End of script
