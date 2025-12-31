<?xml version="1.0" encoding="utf-8"?>
<rsd xmlns="http://archipelago.phrasewise.com/rsd" version="1.0">
	<service>
		<engineName>WordPress</engineName>
		<engineLink>https://wordpress.org/</engineLink>
		<homePageLink>https:/user/assets</homePageLink>

		<apis>
			<api
				name="WordPress"
				blogID="1"
				preferred="true"
				apiLink="https:/user/assets/xmlrpc.php" />

			<api
				name="Movable Type"
				blogID="1"
				preferred="false"
				apiLink="https:/user/assets/xmlrpc.php" />

			<api
				name="MetaWeblog"
				blogID="1"
				preferred="false"
				apiLink="https:/user/assets/xmlrpc.php" />

			<api
				name="Blogger"
				blogID="1"
				preferred="false"
				apiLink="https:/user/assets/xmlrpc.php" />

			<api
				name="WP-API"
				blogID="1"
				preferred="false"
				apiLink="https:/user/assets/wp-json/" />
		</apis>
	</service>
</rsd>
