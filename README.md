# Postgresql Clustering Tutorial
## Introduction
This set of files goes with the YouTube Postgresql clustering tutorial covering a basic introduction to Postgresql and why to use it for clustering, followed by introduction of repmgr and barman, two programs that automate a lot of the work of backups, replicas and failovers and also some scenarios for setting up a proxy and using healthchecks to identify the primary and secondary replicas.

## Files
### PostgresTester
The PostgresTester folder contains the Visual Studio solution with the books test application. Unlike the tutorial, the connection strings have all been moved to Program.cs and hard-coded. This makes things easier because json doesn't support commenting out so moving them into a code file will make it quick to comment out/uncomment the lines you want to use for the test application.

This Solution was written for Visual Studio 2022 and will work in the Community Edition. It might or might not work in earlier versions of Visual Studio. It also requires the dotnet core 6.0 SDK.

### Vagrant
This folder contains the vagrant files for each of the test machines. Note that the vagrant file only contains the basic setup of each VM with a network address, postgresql, barman and repmgr. You can use this machines but will need to follow along with the tutorial to recreate the various parts of setup like changing the configuration files, setting up ssh etc.

These are setup for VirtualBox but should not need much tweaking to work with other Hypervisors.

To start a vagrant machine, go into the relevant vagrant folder, open a command prompt and run `vagrant up` after installing [Vagrant](https://developer.hashicorp.com/vagrant/downloads?product_intent=vagrant)

To *stop* a machine with deleting it, run `vagrant halt` otherwise if you want to destroy it completely you can also run `vagrant destroy` but you could also delete the VM manually from VirtualBox and then delete the .vagrant folder inside the vagrant directory for the VM you are destroying.

## Virtual machines
### postgres1, 2, 3
These are debian boxes running postgresql 13 and have barman-cli installed (to work with `repmgr standby clone`)

### haproxy
This is a VM that only runs haproxy configured with a single configuration file, the 3 different types are included in the vagrant folder so they can be copied into the VM using the mapped `/vagrant` folder as per the tutorial.

It goes without saying that for this to work, the IP addresses or hostnames need to be setup correctly using either DNS or the hosts file and that the network and any firewalls allow access from the haproxy server to the postgres servers. Also, remember that `pg_hba.conf` needs to allow connections from the haproxy server unless you have already used a whole /24 or /16 subnet in the file.

## Usage in production
Most of the instructions here work for production but there are some other things you need to consider before simply enabling and running a cluster in production:

* Performance is not an issue in the tutorial but high-traffic production databases might require significant hardware
* You need to plan for disk size issues, not just for now but how will you cope if the database outgrows its current disk allocation? It is possible to create "tablespaces" which can be mapped to different disks but this does present additional backup challenges and might not be easy to change on-the-fly.
* Monitoring is critical for production systems. You can set them up well but if you don't know there is a problem, you won't be able to fix them.
* *Ongoing* testing is crucial to reducing the risk of problems. Having test environments setup either in addition to the main cluster or even connected to it allows you to *continue* to know that you backup, recovery and monitoring systems are all working. You should encourage everyone in the team to be familiar with the processes because database problems are a serious risk for almost every organisation and the more people who know what they are doing, the easier it is to both resolve a failure and also provide additional expertise so 1 person doesn't make a mistake.
* The security of the network is also paramount. Using a completely isolated virtual network with firewall/access point is a good starting point but who gets access to it and what risk might that present? Who is allowed to be sudo? How do you control who is allowed to do what and when? You also need to be really careful with the use of "trust" in `pg_hba.conf` which is an open door so as far as possible these need to be restricted to as few databases, users and source addresses as possible.
* Setting postgres performance parameters is not always clear-cut but there are some guides available on the internet that provide a rule-of thumb like "Set the max query memory to 75% of the machine RAM". Unless you understand how to monitor these parameters then stick with the guidelines or pay somebody to consult on them. We setup about 10 parameters when we created our production database, some of which might not have been a problem for us but you shouldn't have to change too much.
* Documentation is critical for repeatability and knowing what to do when the alarms start going off and it needs to be fixed NOW. Don't be left trying to remember where you wrote down the failover command or how to find the login credentials for your database servers. Document it all and have it to hand, even print it out in a small book. Decide and communicate how you know who will own the problem if it happens, you don't want 2 people trying to fix it at the same time. Tools like Teams/Slack etc. are useful for communicating what you are doing when things go wrong.

