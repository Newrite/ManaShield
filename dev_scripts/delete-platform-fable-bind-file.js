let fs = require('fs-extra');

if (fs.existsSync('./jsc/SkyrimPlatform.js')) {
	fs.removeSync('./jsc/SkyrimPlatform.js')
}

if (fs.existsSync('./jsc/SkyrimPlatform.js.map')) {
	fs.removeSync('./jsc/SkyrimPlatform.js.map')
}