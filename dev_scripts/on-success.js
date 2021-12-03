let fs = require('fs-extra');
let path = require('path');

if (fs.existsSync('./jsc/SkyrimPlatform.js.ts')) {
	fs.removeSync('./jsc/SkyrimPlatform.js.ts')
}

function writeFileSyncRecursive(filename, content, charset) {
	filename.split(path.sep).slice(0,-1).reduce( (last, folder)=>{
		let folderPath = last ? (last + path.sep + folder) : folder
		if (!fs.existsSync(folderPath)) fs.mkdirSync(folderPath)
		return folderPath
	})
	
	fs.writeFileSync(filename, content, charset)
}

let config = require('../sp-fableconfig.json')
let seRoot = config.seRoot;
let outFile = config.outdir+config.outfile;
let pluginName = outFile ? outFile.replace(/^.*[\\\/]/, '') : undefined

let outDir = '';

if (!pluginName) {
	outDir = config.outdir;
	pluginName = outDir.replace(/^.*[\\\/]/, '');
}

if (!seRoot) {
	throw new Error('Missing `seRoot` in sp-fableconfig.json');
}

console.log(`Installing ${pluginName}`);
if (!outDir) {
    writeFileSyncRecursive(path.join(seRoot, 'Data\\Platform\\PluginsDev', pluginName), fs.readFileSync(outFile));
}
else {
    fs.copySync(`./dist/${pluginName}`, path.join(seRoot, 'Data\\Platform\\PluginsDev', pluginName));
}
