4. Set Permissions for Both Files

Fix path
sudo chown www-data:www-data /var/www/TowBoatUSApp/users.db
sudo chmod 660 /var/www/TowBoatUSApp/users.db

sudo chown www-data:www-data /var/www/TowBoatUSApp/salvage.db
sudo chmod 660 /var/www/TowBoatUSApp/salvage.db