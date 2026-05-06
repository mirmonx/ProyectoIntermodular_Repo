const cloudinary = require("cloudinary").v2;
require("dotenv").config();

cloudinary.config({
  cloud_name: "dql0mgp6n",
  api_key: "677926168842928",
  api_secret: "r6beBAJ3Gsh7JJZqncPeH1SepjI",
});

module.exports = cloudinary;
