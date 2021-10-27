require 'plist'

provisioning_plist_path=ARGV[0]
export_options_plist_path=ARGV[1]

provisioning_plist = Plist.parse_xml("#{provisioning_plist_path}")

export_options_plist = Plist.parse_xml("#{export_options_plist_path}")
puts "export_options_plist: #{export_options_plist}"

(provisioning_plist['Entitlements']['application-identifier']).slice! "#{provisioning_plist['TeamIdentifier'][0]}."
app_id = provisioning_plist['Entitlements']['application-identifier']

export_options_plist['provisioningProfiles'] = { "#{app_id}"=> provisioning_plist["Name"] }

export_options_plist['teamID'] = provisioning_plist['TeamIdentifier'][0]

if provisioning_plist['Entitlements']['get-task-allow']
    export_options_plist['method'] = "development"
elsif provisioning_plist['ProvisionsAllDevices']
    export_options_plist['method'] = "enterprise"
elsif provisioning_plist['ProvisionedDevices']
    export_options_plist['method'] = "ad-hoc"
else
    export_options_plist['method'] = "app-store"
end

export_options_plist.save_plist("#{export_options_plist_path}")
